using DigiFamily.Helpers;
using DigiFamily.Helpers.Utils;
using DigiFamily.ViewModels;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiFamily.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel ViewModel;
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new LoginViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                ViewModel.IsRememberMe = Convert.ToBoolean(await SecureStorage.GetAsync("IsRemember"));
                if (ViewModel.IsRememberMe)
                {
                    ViewModel.Username = await SecureStorage.GetAsync("Username");
                    ViewModel.Password = await SecureStorage.GetAsync("Password");
                }
                else
                {
                    ViewModel.Username = string.Empty;
                    ViewModel.Password = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        private void passwordEye_Clicked(object sender, System.EventArgs e)
        {
            if (PasswordEntry.IsPassword)
            {
                PasswordEntry.IsPassword = false;
                passwordEye.Source = new FontImageSource
                {
                    FontFamily = "FontAwesome",
                    Glyph = IconFont.EyeOff,
                    Color = CommonHelper.GetResource<Color>("IconColor"),
                    Size = 20
                };
            }
            else
            {
                PasswordEntry.IsPassword = true;
                passwordEye.Source = new FontImageSource
                {
                    FontFamily = "FontAwesome",
                    Glyph = IconFont.Eye,
                    Color = CommonHelper.GetResource<Color>("IconColor"),
                    Size = 20
                };
            }
        }
    }
}