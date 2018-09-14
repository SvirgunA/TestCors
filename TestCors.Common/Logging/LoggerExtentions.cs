using Microsoft.Extensions.Logging;
using System;

namespace TestCors.Common.Logging
{
    public static class LoggerExtentions
    {
        public static void LogException(this ILogger logger, Exception ex)
        {
            logger.Log(LogLevel.Critical, new EventId(), ex.Message, ex, (s, e) => e.Message);
        }
    }
}
