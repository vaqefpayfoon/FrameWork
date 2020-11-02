using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class FiscalYearViewModel : BaseViewModel
    {

        [Display(Name = nameof(CompanyTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? CompanyId { get; set; }

        [Display(Name = nameof(CompanyTitle), ResourceType = typeof(Resources.Labels))]
        public string CompanyTitle { get; set; }


        [Display(Name = nameof(StartDate), ResourceType = typeof(Resources.Labels))]
        public DateTime? StartDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string JalaliStartDate { get; set; }
        [Display(Name = nameof(EndDate), ResourceType = typeof(Resources.Labels))]
        public DateTime? EndDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string JalaliEndDate { get; set; }






    }
}
