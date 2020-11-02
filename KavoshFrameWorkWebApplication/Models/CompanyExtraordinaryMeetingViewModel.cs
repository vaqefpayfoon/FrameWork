using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class CompanyExtraordinaryMeetingViewModel : BaseBaseViewModel
    {
        [Display(Name = nameof(Date), ResourceType = typeof(Resources.Labels))]
        public DateTime Date { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string JalaliDate { get; set; }
       
        [Display(Name = nameof(MainGroupTitle), ResourceType = typeof(Resources.Labels))]
        public string MainGroupTitle { get; set; }
        [Display(Name = nameof(MainGroupTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? MainGroupId { get; set; }

        [Display(Name = nameof(SubGroupTitle), ResourceType = typeof(Resources.Labels))]
        public string SubGroupTitle { get; set; }
        [Display(Name = nameof(SubGroupTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? SubGroupId { get; set; }

        [Display(Name = nameof(FiscalYearTitle), ResourceType = typeof(Resources.Labels))]
        public string FiscalYearTitle { get; set; }
        [Display(Name = nameof(FiscalYearTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? FiscalYearId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = nameof(Decisions), ResourceType = typeof(Resources.Labels))]
        public string Decisions { get; set; }
        [Display(Name = nameof(CompanyTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? CompanyId { get; set; }
        [Display(Name = nameof(CompanyTitle), ResourceType = typeof(Resources.Labels))]
        public string CompanyTitle { get; set; }

        [Display(Name = nameof(MeetingSubject), ResourceType = typeof(Resources.Labels))]
        public MeetingSubject MeetingSubject { get; set; }

        [Display(Name = nameof(CapitalChangeFrom), ResourceType = typeof(Resources.Labels))]
        public long CapitalChangeFrom { get; set; }

        [Display(Name = nameof(CapitalChangeTo), ResourceType = typeof(Resources.Labels))]
        public long CapitalChangeTo { get; set; }
        [Display(Name = nameof(AssemblyType), ResourceType = typeof(Resources.Labels))]
        public AssemblyType AssemblyType { get; set; }
    }
}
