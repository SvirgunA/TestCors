using System;
using System.Collections.Generic;
using System.Text;

namespace TestCors.Common.Constants
{
    public class ConfigurationFilesKeys
    {
        public static class ApplicationSettings
        {
            public const string APPLICATION_ROOT_KEY = @"application_root";
            public const string AUTH_SETTINGS_SECTION_KEY = @"auth_settings";
            public const string MAIL_SETTINGS_SECTION_KEY = @"mail_settings";
        }

        public static class ConnectionSettings
        {
            public const string DATABASE_SECTION_NAME = @"database";
        }
    }
}
