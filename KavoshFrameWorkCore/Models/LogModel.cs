using System;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.Models
{
    public class LogModel : IAdminEntity
    {
        public int Id { get; set; }

        public string Level { get; set; }

        public string Message { get; set; }

        public DateTime Timestamp { get; set; }

        public string Exception { get; set; }

        public string Properties { get; set; }
        public string LogEvent { get; set; }
        public string MessageTemplate { get; set; }
        public string ActionName { get; set; }
        public string UserName { get; set; }
        public string ActionId { get; set; }
        public string IP { get; set; }
        public string RequestId { get; set; }
        public string RequestPath { get; set; }
        public string date_str { get; set; }
    }
}
