using System;
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

        public Command SendMessageCommand { get; }

        public ChatViewModel()
        {
            ChatMessage = new ChatMessage();
            Messages = new ObservableRangeCollection<ChatMessage>();
            SendMessageCommand = new Command(SendMessage);
        }

        void SendMessage()
        {

        }
    }
}
