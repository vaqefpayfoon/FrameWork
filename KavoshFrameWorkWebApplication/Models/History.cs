using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class History
    {
        public DateTime? DateTime { get; set; }

        public long? UserId { get; set; }

        public object Value { get; set; }

        public object OldValue { get; set; }
        public string DateTimeStr { get; set; }

        public string User { get; set; }
    }
}
