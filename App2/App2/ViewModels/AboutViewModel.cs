using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App2.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Aprops";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            OnAlertYesNoClicked = new Command(async () => await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No"));
        }

        private Task DisplayAlert(string v1, string v2, string v3, string v4)
        {
            throw new NotImplementedException();
        }

        public ICommand OnAlertYesNoClicked { get; set;}
        public ICommand OpenWebCommand { get; }
    }
}