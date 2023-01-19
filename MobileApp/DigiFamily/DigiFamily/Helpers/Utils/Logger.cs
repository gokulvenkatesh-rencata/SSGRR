using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DigiFamily.Helpers.Utils
{
    public class Logger
    {
        static Logger()
        {
            Analytics.SetEnabledAsync(true);
        }
        public static void Info(string message)
        {
            Debug.WriteLine(message);
            Analytics.TrackEvent(message);
        }
        public static void Info(string msg, IDictionary<string, string> properties = null)
        {
            Debug.WriteLine(msg + " - " + JsonConvert.SerializeObject(properties));
            Analytics.TrackEvent(msg, properties);
        }
        public static void Info(object value)
        {
            Debug.WriteLine(value);
            Analytics.TrackEvent(JsonConvert.SerializeObject(value));
        }
        public static void Error(object value)
        {
            Debug.WriteLine(value);
            Crashes.TrackError(value as Exception);
        }
        public static void LogNavigation(string fromPage, string toPage, string trigger)
        {
            Analytics.TrackEvent($"Navigated to {toPage} from {fromPage} triggered from {trigger}");
        }
    }
}
