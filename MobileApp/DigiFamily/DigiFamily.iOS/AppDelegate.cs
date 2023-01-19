using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using UIKit;

namespace DigiFamily.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            AppCenter.Start("129bb46c-0ac9-4cd6-a686-ff146007f750", typeof(Analytics), typeof(Crashes));
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            return base.FinishedLaunching(app, options);
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

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Console.WriteLine("Domain Unhandled Exception:\r\n" + ex.Message);
            Crashes.TrackError(ex, new Dictionary<string, string> {
                {"Source", "CurrentDomain_UnhandledException" },
                {"Content", ex.Message }
            });
        }
    }
}
