using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;

namespace TestCors.Common.Logging
{
    public class DbLogger : ILogger
    {
        private readonly string _connectionString;
        private readonly string _serviceName;

        public DbLogger(string connectionString, string serviceName)
        {
            this._connectionString = connectionString;
            this._serviceName = serviceName;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this._connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(@"INSERT INTO dbo.Logs(DateTime,ServiceName,Severity,Message,CallStack)
                                                   VALUES (@dateTime,@serviceName,@severity,@message,@callStack)", connection);
                    command.CommandTimeout = 1;

                    command.Parameters.AddWithValue("@dateTime", DateTime.Now);
                    command.Parameters.AddWithValue("@serviceName", _serviceName);
                    command.Parameters.AddWithValue("@severity", logLevel.ToString());
                    if (exception == null)
                    {
                        command.Parameters.AddWithValue("@message", formatter(state, null));
                        command.Parameters.AddWithValue("@callStack", string.Empty);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@message", exception.Message);
                        command.Parameters.AddWithValue("@callStack", exception.StackTrace ?? string.Empty);
                    }

                    command.ExecuteNonQuery();
                }
            }
            catch(Exception e)
            {
                var a = e.Message;
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }
    }
}
