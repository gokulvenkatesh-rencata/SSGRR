using DigiFamily.Helpers;
using DigiFamily.Helpers.Utils;
using DigiFamily.Services;
using DigiFamily.Views;
using DigiFamily.Views.Common;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DigiFamily
{
    public partial class App : Application
    {
        public static ToastPage ToastPage = null;
        public static AlertPopupPage AlertPopup = null;
        public static ForgotPasswordPopupPage ForgotPasswordPopup=null;
        public App()
        {
            try
            {
                InitializeComponent();
                InitDependencies();
                Connectivity.ConnectivityChanged += ConnectivityChanged;
                Task<string> task = Task.Run(async () => await CommonHelper.GetAuthTokenAsync());
                string oauthToken = task.Result;
                if (!string.IsNullOrEmpty(oauthToken))
                    MainPage = new AppShellMain();
                else
                    MainPage = new AppShellLogin();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        protected override void OnStart()
        {
            OnResume();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void InitDependencies()
        {
            CommonHelper.SetTheme(OSAppTheme.Dark);
            DependencyService.Register<HttpClientService>();
            DependencyService.Register<AccountService>();
        }

        protected async void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }
    }
}
