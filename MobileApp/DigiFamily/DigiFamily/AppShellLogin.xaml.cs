using DigiFamily.Helpers;
using DigiFamily.Views;
using System;
using Xamarin.Forms;

namespace DigiFamily
{
    public partial class AppShellLogin : Shell
    {
        public AppShellLogin()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(UserDetailsPage), typeof(UserDetailsPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await CommonHelper.LogoutAsync(true);
        }
    }
}
