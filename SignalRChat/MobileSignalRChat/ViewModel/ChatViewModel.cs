using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MobileSignalRChat.Model;
using MvvmHelpers;
using Xamarin.Forms;


namespace MobileSignalRChat.ViewModel
{
    public class ChatViewModel : BaseViewModel
    {
        HubConnection hubConnection;

        public ChatMessage ChatMessage { get; }

        public ObservableRangeCollection<ChatMessage> Messages { get; }

        bool isConnected;
        public bool IsConnected
        {
            get => isConnected;
            set
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    SetProperty(ref isConnected, value);
                });
            }
        }

        public Command SendMessageCommand { get; }
        public Command ConnectCommand { get; }
        public Command DisconnectCommand { get; }
        Random random;

        public ChatViewModel()
        {
            ChatMessage = new ChatMessage();
            Messages = new ObservableRangeCollection<ChatMessage>();
            SendMessageCommand = new Command(async () => await SendMessage());
            ConnectCommand = new Command(async () => await Connect());
            DisconnectCommand = new Command(async () => await Disconnect());
            random = new Random();

            string url = "http://localhost:5000/chatHub";
            if (Device.RuntimePlatform == Device.Android)
                url = "http://10.0.2.2:5000/chatHub";

            hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            hubConnection.Closed += async (error) =>
            {
                sendLocalMessage("Connection closed...");
                IsConnected = false;
                await Task.Delay(random.Next(0, 5) * 1000);
                await Connect();
            };

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var finalMsg = $"{user} says {message}";
                sendLocalMessage(finalMsg);
            });
        }

        async Task Connect()
        {
            if (IsConnected)
                return;
            try
            {
                await hubConnection.StartAsync();
                IsConnected = true;
                sendLocalMessage("Connected...");
            }
            catch (Exception ex)
            {
                sendLocalMessage("Connection error: {ex.Message}");
            }
        }

        async Task Disconnect()
        {
            if (!IsConnected)
                return;

            await hubConnection.StopAsync();
            IsConnected = false;
            sendLocalMessage("Disconnected...");
        }

        async Task SendMessage()
        {
            try
            {
                IsBusy = true;
                await hubConnection.InvokeAsync("SendMessage",
                    ChatMessage.User,
                    ChatMessage.Message);
            }
            catch (Exception ex)
            {
                sendLocalMessage($"Send failed: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void sendLocalMessage(string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Messages.Add(new ChatMessage
                {
                    Message = message
                });
            });
        }
    }
}
