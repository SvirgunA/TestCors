using System;

namespace TestCors.Models
{
    public class LogEntry : IEntity
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string ServiceName { get; set; }
        public string Severity { get; set; }
        public string Message { get; set; }
        public string CallStack { get; set; }
    }
}
