using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }
        public async void TesteNoe(object sender, EventArgs a)
        {
            await Navigation.PushAsync(new MoneysDetection());
        }
    }
}