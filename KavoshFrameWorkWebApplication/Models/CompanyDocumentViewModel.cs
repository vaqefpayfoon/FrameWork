using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class CompanyDocumentViewModel : BaseStatusViewModel
    {
        [Display(Name = nameof(Title), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string Title { get; set; }
        [Display(Name = nameof(MainGroupTitle), ResourceType = typeof(Resources.Labels))]
        public string MainGroupTitle { get; set; }
        [Display(Name = nameof(MainGroupTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int MainGroupId { get; set; }

        [Display(Name = nameof(SubGroupTitle), ResourceType = typeof(Resources.Labels))]
        public string SubGroupTitle { get; set; }
        [Display(Name = nameof(SubGroupTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int SubGroupId { get; set; }

        [Display(Name = nameof(FiscalYearTitle), ResourceType = typeof(Resources.Labels))]
        public string FiscalYearTitle { get; set; }
        [Display(Name = nameof(FiscalYearTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? FiscalYearId { get; set; }
        public string FileName { get; set; }
        public string MediaType { get; set; }
        public bool HasMedia { get; set; }
    }
}
