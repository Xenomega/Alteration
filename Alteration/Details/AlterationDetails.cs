using System.Collections.Generic;
namespace Alteration.Details
{
    public abstract class AlterationDetails
    {
        public static bool showUpdateBoxOnStartup;

        private static float _version = 0.0525f;
        public static float Version
        {
            get { return _version; }
            set { _version = value; }
        }

        private static string _username;
        public static string Username
        {
            get { return _username; }
            set { _username = value; }
        }


        private static string _password;
        public static string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private static Dictionary<string, string> _authSettings;
        public static Dictionary<string, string> AuthSettings
        {
            get { return _authSettings; }
            set { _authSettings = value; }
        }

    }
}