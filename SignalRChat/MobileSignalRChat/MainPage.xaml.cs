using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileSignalRChat.ViewModel;
using Xamarin.Forms;

namespace MobileSignalRChat
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        ChatViewModel vm;
        ChatViewModel VM
        {
            get => vm ?? (vm = (ChatViewModel)BindingContext);
        }

        public MainPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.ConnectCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            VM.DisconnectCommand.Execute(null);
        }
    }
}
