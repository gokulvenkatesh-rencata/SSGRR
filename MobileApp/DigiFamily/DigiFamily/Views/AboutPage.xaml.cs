using DigiFamily.ViewModels;
using System;
using Xamarin.Forms;

namespace DigiFamily.Views
{
    public partial class AboutPage : ContentPage
    {
        private AboutViewModel ViewModel;
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new AboutViewModel();
        }
    }
}