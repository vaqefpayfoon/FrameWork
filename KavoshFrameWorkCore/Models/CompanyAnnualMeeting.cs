using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KavoshFrameWorkCore.Models
{
    [DisplayName("مجمع عمومی سالیانه")]
    public class CompanyAnnualMeeting : BaseBaseEntity
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public MainGroup MainGroup { get; set; }
        public int MainGroupId { get; set; }

        public SubGroup SubGroup { get; set; }
        public int SubGroupId { get; set; }

        public FiscalYear FiscalYear { get; set; }
        public int FiscalYearId { get; set; }

        public long HoldingShares { get; set; }
        public long DividedProfit { get; set; }
        public long BoardBonus { get; set; }
        public double TotalProfit { get; set; }
        public long BoardRight { get; set; }

        public Auditor PrimaryAuditor { get; set; }
        public int PrimaryAuditorId { get; set; }

        public Auditor SecondaryAuditor { get; set; }
        public int SecondaryAuditorId { get; set; }

        public Newspaper Newspaper { get; set; }
        public int NewspaperId { get; set; }
        public Newspaper Newspaper2 { get; set; }
        public int NewspaperId2 { get; set; }

        public DateTime Date { get; set; }
        public double? SocialResponsibility { get; set; }
        public double? AccountingRight { get; set; }
        public AssemblyType AssemblyType { get; set; }
    }
}
