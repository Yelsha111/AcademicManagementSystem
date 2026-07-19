using System;
using System.Configuration;
using AcademicManagement.Services;

namespace AcademicManagement.Config
{
    // Every form calls FirebaseConfig.GetService() instead of constructing its own
    // FirebaseService - this way there's exactly one HttpClient/connection for the
    // whole app, and the URL/secret only live in one place: App.config.
    public static class FirebaseConfig
    {
        private static FirebaseService _instance;

        public static FirebaseService GetService()
        {
            if (_instance == null)
            {
                var url = ConfigurationManager.AppSettings["FirebaseUrl"];
                var secret = ConfigurationManager.AppSettings["FirebaseSecret"];

                if (string.IsNullOrWhiteSpace(url))
                {
                    throw new InvalidOperationException(
                        "FirebaseUrl is missing from App.config. Add it under <appSettings>.");
                }

                _instance = new FirebaseService(url, secret);
            }

            return _instance;
        }
    }
}
