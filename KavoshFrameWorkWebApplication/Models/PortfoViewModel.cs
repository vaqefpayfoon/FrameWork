using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class PortfoViewModel : BaseBaseViewModel
    {
        public PortfoViewModel()
        {
            PortfoShareholderPairs = new List<PortfoShareholderPairViewModel>();
            CompanyShareholders = new List<CompanyShareholderViewModel>();
            PortfoShareholderViewModels = new List<PortfoShareholderViewModel>();
        }
        [Display(Name = nameof(CompanyTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? CompanyId { get; set; }
        [Display(Name = nameof(CompanyTitle), ResourceType = typeof(Resources.Labels))]
        public string CompanyTitle { get; set; }
        [Display(Name = nameof(AgentsNumber), ResourceType = typeof(Resources.Labels))]
        public int AgentsNumber { get; set; }
        [Display(Name = nameof(AggregatePercentageCalculationType), ResourceType = typeof(Resources.Labels))]
        public AggregatePercentageCalculationType AggregatePercentageCalculationType { get; set; }
        [Display(Name = nameof(AggregatePercentage), ResourceType = typeof(Resources.Labels))]
        public double AggregatePercentage { get; set; }
        [Display(Name = nameof(ShareholderHoldingCommunication), ResourceType = typeof(Resources.Labels))]
        public bool ShareholderHoldingCommunication { get; set; }
        [Display(Name = nameof(UnderManagementHoldingCommunication), ResourceType = typeof(Resources.Labels))]
        public bool UnderManagementHoldingCommunication { get; set; }

        [Display(Name = nameof(ShareholderTitle), ResourceType = typeof(Resources.Labels))]
        public string ShareholderTitle { get; set; }

        [Display(Name = nameof(Shares), ResourceType = typeof(Resources.Labels))]
        public long Shares { get; set; }

        [Display(Name = nameof(OwnershipPercentage), ResourceType = typeof(Resources.Labels))]
        public double OwnershipPercentage { get; set; }

        [Display(Name = nameof(CompanyTypeTitle), ResourceType = typeof(Resources.Labels))]
        public string CompanyTypeTitle { get; set; }

        [Display(Name = nameof(CurrentCapital), ResourceType = typeof(Resources.Labels))]
        public long CurrentCapital { get; set; }
        public List<KeyValueViewModel> ValidShareholderTreeNodes { get;  set; }

        public List<PortfoShareholderPairViewModel> PortfoShareholderPairs { get; set; }
        public List<CompanyShareholderViewModel> CompanyShareholders { get; set; }
        public List<PortfoShareholderViewModel> PortfoShareholderViewModels { get; set; }
       
    }

    public class PortfoShareholderPairViewModel : BaseBaseEntity
    {
        public int CompanyId { get; set; }
        public int ShareholderId { get; set; }
        public int ShareholderCompanyShareholderId { get; set; }

        public int Row { get; set; }
        public double Column { get; set; }
        public string Pair { get; set; }
        public double Value { get; set; }
        public string ShareholderCompanyTitle { get;  set; }
        public bool IsMain { get; set; }
        public string Title { get; set; }
    }
    public class PortfoShareholderViewModel : BaseBaseEntity
    {
        public int CompanyId { get; set; }
        public int ShareholderId { get; set; }
        public int ShareholderCompanyShareholderId { get; set; }

        public int Row { get; set; }
        public double Column { get; set; }
        public string Pair { get; set; }
        public double Value { get; set; }
        public string ShareholderCompanyTitle { get; set; }
        public bool IsMain { get; set; }
        public string Title { get; set; }
        public bool show { get; set; } = true;
    }
}
