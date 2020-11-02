using Microsoft.AspNetCore.Mvc.Rendering;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class CompanyShareViewModel : BaseStatusViewModel
    {
        [Display(Name = nameof(Date), ResourceType = typeof(Resources.Labels))]
        public DateTime? Date { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string   JalaliDate { get; set; }

        [Display(Name = nameof(PreviousCapital), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public long PreviousCapital { get; set; }
        [Display(Name = nameof(PreviousShares), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public long PreviousShares { get; set; }

        [Display(Name = nameof(CurrentShares), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public long CurrentShares { get; set; }


        [Display(Name = nameof(CurrentCapital), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public long CurrentCapital { get; set; }
        [Display(Name = nameof(CapitalChangePercentage), ResourceType = typeof(Resources.Labels))]
        public double CapitalChangePercentage { get; set; }
        [Display(Name = nameof(TotalShares), ResourceType = typeof(Resources.Labels))]
        public int TotalShares { get; set; }

        [Display(Name = nameof(CapitalIncreaseFromAccumulatedProfits), ResourceType = typeof(Resources.Labels))]
        public double CapitalIncreaseFromAccumulatedProfits { get; set; }
        [Display(Name = nameof(CapitalIncreaseFromCash), ResourceType = typeof(Resources.Labels))]
        public double CapitalIncreaseFromCash { get; set; }
        [Display(Name = nameof(CapitalIncreaseFromShareholderReceivables), ResourceType = typeof(Resources.Labels))]
        public double CapitalIncreaseFromShareholderReceivables { get; set; }
        [Display(Name = nameof(CapitalIncreaseFromAssetsRevaluation), ResourceType = typeof(Resources.Labels))]
        public double CapitalIncreaseFromAssetsRevaluation { get; set; }

        [Display(Name = nameof(ParValue), ResourceType = typeof(Resources.Labels))]
        public long ParValue { get; set; }
        public KavoshFrameWorkCore.FormStatus FormStatusFilter { get; set; }

        public string ChangeAsset { get; set; }
        public double CountAllShare { get; set; }
        public int Year { get; set; }
        public List<SelectListItem> Month { get; set; }

    }
}
