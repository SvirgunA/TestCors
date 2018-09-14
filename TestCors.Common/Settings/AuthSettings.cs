using System;
using System.Collections.Generic;
using System.Text;

namespace TestCors.Common.Settings
{
    public class AuthSettings
    {
        public string TOKEN_ISSUER { get; set; }
        public string TOKEN_AUDIENCE { get; set; }
        public string ENCRYPTION_KEY { get; set; }
        public int ID_TOKEN_LIFETIME_MINUTES { get; set; }
    }
}
