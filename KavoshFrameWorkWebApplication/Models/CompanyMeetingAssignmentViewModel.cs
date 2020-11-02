using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class CompanyMeetingAssignmentViewModel : BaseStatusViewModel
    {
        [Display(Name = "MeetingDate", ResourceType = typeof(Resources.Labels))]
        public DateTime Date { get; set; }
        [Display(Name = nameof(Title), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string JalaliDate { get; set; }

        [Display(Name = nameof(Assignments), ResourceType = typeof(Resources.Labels))]
        public string Assignments { get; set; }

        [Display(Name = nameof(Report), ResourceType = typeof(Resources.Labels))]
        public string Report { get; set; }

        [Display(Name = nameof(Actions), ResourceType = typeof(Resources.Labels))]
        public string Actions { get; set; }
        public string FileName { get; set; }
        public string MediaType { get; set; }
        public bool HasMedia { get; set; }
    }
}
