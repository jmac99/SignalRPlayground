using System;
using MobileSignalRChat.Model;
using MvvmHelpers;

namespace MobileSignalRChat.ViewModel
{
    public class ChatViewModel : BaseViewModel
    {
        public ChatMessage ChatMessage { get; }

        public ObservableRangeCollection<ChatMessage> Messages { get; }

        public ChatViewModel()
        {
            ChatMessage = new ChatMessage();
            Messages = new ObservableRangeCollection<ChatMessage>();
        }
    }
}
