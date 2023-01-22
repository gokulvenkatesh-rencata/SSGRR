using DigiFamily.Helpers.Utils;
using DigiFamily.ViewModels;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiFamily.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private SettingsViewModel ViewModel;
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new SettingsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                ViewModel.IsDarkTheme = Preferences.Get("IsDarkTheme", true);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}