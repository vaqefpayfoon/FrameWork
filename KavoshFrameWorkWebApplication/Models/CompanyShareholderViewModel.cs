using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Controllers;
using KavoshFrameWorkWebApplication.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class CompanyShareholderViewModel : BaseStatusViewModel
    {
        [Display(Name = nameof(ShareholderTitle), ResourceType = typeof(Resources.Labels))]
        public int ShareholderId { get; set; }
        [Display(Name = nameof(ShareholderTitle), ResourceType = typeof(Resources.Labels))]
        public string ShareholderTitle { get; set; }


        [Display(Name = nameof(SubShareholderTitle), ResourceType = typeof(Resources.Labels))]
        public int? SubShareholderId { get; set; }
        [Display(Name = nameof(ShareholderTitle), ResourceType = typeof(Resources.Labels))]
        public string SubShareholderTitle { get; set; }


        [Display(Name = nameof(Year), ResourceType = typeof(Resources.Labels))]
        public int Year { get; set; }

        [Display(Name = nameof(Month), ResourceType = typeof(Resources.Labels))]
        public int Month { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = nameof(Shares), ResourceType = typeof(Resources.Labels))]
        public long Shares { get; set; }

        [Display(Name = nameof(OwnershipPercentage), ResourceType = typeof(Resources.Labels))]
        public double OwnershipPercentage { get; set; }


        [Display(Name = nameof(IncludeInFormula), ResourceType = typeof(Resources.Labels))]
        public bool IncludeInFormula { get; set; }

        [Display(Name = nameof(IsMainShareholder), ResourceType = typeof(Resources.Labels))]
        public bool IsMainShareholder { get; set; }
        public double TotalShare { get; internal set; }

        public int Row { get; set; }
        public double CurrentCapital { get; set; }
    }
    public class CompanyShareholderReport : BaseStatusViewModel
    {
        [Display(Name = nameof(ShareholderTitle), ResourceType = typeof(Resources.Labels))]
        public int ShareholderId { get; set; }
        [Display(Name = nameof(ShareholderTitle), ResourceType = typeof(Resources.Labels))]
        public string ShareholderTitle { get; set; }

        [Display(Name = nameof(Year), ResourceType = typeof(Resources.Labels))]
        public int Year { get; set; }

        [Display(Name = nameof(Month), ResourceType = typeof(Resources.Labels))]
        public int Month { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = nameof(Shares), ResourceType = typeof(Resources.Labels))]
        public long Shares { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = nameof(Shares), ResourceType = typeof(Resources.Labels))]
        public long OldShares { get; set; }

        [Display(Name = nameof(Year), ResourceType = typeof(Resources.Labels))]
        public int YearOld { get; set; }

        [Display(Name = nameof(Month), ResourceType = typeof(Resources.Labels))]
        public int MonthOld { get; set; }
    }
}
