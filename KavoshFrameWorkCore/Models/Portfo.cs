using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KavoshFrameWorkCore.Models
{
    [DisplayName("پرتفو")]
    public class Portfo : BaseBaseEntity
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public int AgentsNumber { get; set; }
        public AggregatePercentageCalculationType AggregatePercentageCalculationType { get; set; }
        public double AggregatePercentage { get; set; }
        public bool ShareholderHoldingCommunication { get; set; }
        public bool UnderManagementHoldingCommunication { get; set; }

        public List<PortfoShareholderPair> PortfoShareholderPairs { get; set; }
    }

    public class PortfoShareholderPair : BaseBaseEntity
    {
        public int PortfoId { get; set; }
        public Portfo Portfo { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public Shareholder Shareholder { get; set; }
        public int ShareholderId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
    public enum AggregatePercentageCalculationType
    {
     
        [Display(Name ="به صورت فرمولی")]
        Formula,
        [Display(Name = "به صورت دستی")]
        Manual
    }

}
