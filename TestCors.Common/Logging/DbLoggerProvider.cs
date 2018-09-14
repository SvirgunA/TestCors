using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestCors.Common.Settings;

namespace TestCors.Common.Logging
{
    public class DbLoggerProvider : ILoggerProvider
    {
        private readonly IOptions<DatabaseSettings> _settings;

        public DbLoggerProvider(IOptions<DatabaseSettings> settings)
        {
            this._settings = settings;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(_settings.Value.CONNECTION_STRING, categoryName);
        }

        public void Dispose()
        {
        }
    }
}
