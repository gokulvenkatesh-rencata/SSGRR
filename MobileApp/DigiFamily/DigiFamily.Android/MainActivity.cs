using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.Net;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using System.Collections.Generic;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter;

namespace DigiFamily.Droid
{
    [Activity(Label = "DigiFamily", Icon = "@mipmap/icon", RoundIcon = "@mipmap/icon_round", Theme = "@style/SplashTheme", LaunchMode = LaunchMode.SingleTop, MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.SetTheme(Resource.Style.MainTheme);
            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this);
            Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;
            LoadApplication(new App());
            AppCenter.Start("03cb6dce-2821-4778-85cd-d6c9d9d06b08", typeof(Analytics), typeof(Crashes));
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException; 
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Console.WriteLine("Domain Unhandled Exception:\r\n" + ex.Message);
            Crashes.TrackError(ex, new Dictionary<string, string> {
                {"Source", "CurrentDomain_UnhandledException" },
                {"Content", ex.Message }
            });
        }
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Console.WriteLine("TaskScheduler Unhandled Exception:\r\n" + ex.Message);
            Crashes.TrackError(ex, new Dictionary<string, string> {
                {"Source", "TaskScheduler_UnobservedTaskException" },
                {"Content", ex.Message }
            });
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}