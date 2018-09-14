using System;
using System.Collections.Generic;
using System.Text;

namespace TestCors.Common.Settings
{
    public class DatabaseSettings
    {
        public string CONNECTION_STRING { get; set; }
        public string DATABASE_SCHEMA { get; set; }
        public bool AUTO_MIGRATIONS { get; set; }
    }
}
