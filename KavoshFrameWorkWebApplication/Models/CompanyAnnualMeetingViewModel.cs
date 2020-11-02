using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class CompanyAnnualMeetingViewModel : BaseBaseViewModel
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

       
        [Display(Name = nameof(CompanyTitle), ResourceType = typeof(Resources.Labels))]
        public int CompanyId { get; set; }
        [Display(Name = nameof(CompanyTitle), ResourceType = typeof(Resources.Labels))]
        public string CompanyTitle { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = nameof(HoldingShares), ResourceType = typeof(Resources.Labels))]
        public long HoldingShares { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = nameof(DividedProfit), ResourceType = typeof(Resources.Labels))]
        public long DividedProfit { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = nameof(BoardBonus), ResourceType = typeof(Resources.Labels))]
        public long BoardBonus { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = nameof(TotalProfit), ResourceType = typeof(Resources.Labels))]
        public double TotalProfit { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = nameof(BoardRight), ResourceType = typeof(Resources.Labels))]
        public long BoardRight { get; set; }

        [Display(Name = nameof(PrimaryAuditorTitle), ResourceType = typeof(Resources.Labels))]
        public string PrimaryAuditorTitle { get; set; }

        [Display(Name = nameof(PrimaryAuditorTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? PrimaryAuditorId { get; set; }

        [Display(Name = nameof(SecondaryAuditorTitle), ResourceType = typeof(Resources.Labels))]
        public string SecondaryAuditorTitle { get; set; }

        [Display(Name = nameof(SecondaryAuditorTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? SecondaryAuditorId { get; set; }

        [Display(Name = nameof(NewspaperTitle), ResourceType = typeof(Resources.Labels))]
        public string NewspaperTitle { get; set; }

        [Display(Name = nameof(NewspaperTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? NewspaperId { get; set; }
        [Display(Name = nameof(NewspaperTitle), ResourceType = typeof(Resources.Labels))]
        public string NewspaperTitle2 { get; set; }

        [Display(Name = nameof(NewspaperTitle), ResourceType = typeof(Resources.Labels))]
        public int? NewspaperId2 { get; set; }
        [Display(Name = nameof(SocialResponsibility), ResourceType = typeof(Resources.Labels))]
        public double SocialResponsibility { get; set; }
        [Display(Name = nameof(AccountingRight), ResourceType = typeof(Resources.Labels))]
        public double AccountingRight { get; set; }
        public double CurrentCapital { get; set; }
        [Display(Name = nameof(AssemblyType), ResourceType = typeof(Resources.Labels))]
        public AssemblyType AssemblyType { get; set; }
    }
}
