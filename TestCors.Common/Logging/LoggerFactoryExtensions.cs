using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestCors.Common.Settings;

namespace TestCors.Common.Logging
{
    public static class LoggerFactoryExtensions
    {
        public static void AddDbLogging(this ILoggerFactory loggerFactory, IOptions<DatabaseSettings> settings)
        {
            loggerFactory.AddProvider(new DbLoggerProvider(settings));
        }
    }
}
