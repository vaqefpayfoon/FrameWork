using KavoshFrameWorkWebApplication.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class LogViewModel : BaseViewModel
    {
        public string Level { get; set; }
        public string Message { get; set; }
        [Display(Name = nameof(UserName), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public DateTime Timestamp { get; set; }
        public string JalaliTimestamp { get; set; }
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
    }
}
