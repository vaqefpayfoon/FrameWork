using System;

namespace KavoshFrameWorkCore.Extensions.DTO
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
