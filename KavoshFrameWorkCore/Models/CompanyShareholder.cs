using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static KavoshFrameWorkCore.Models.Enums;

namespace KavoshFrameWorkCore.Models
{
    [DisplayName("سهامداران عمده")]
    public class CompanyShareholder : BaseBaseEntity
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public Shareholder Shareholder { get; set; }
        public int ShareholderId { get; set; }

        public Shareholder SubShareholder { get; set; }
        public int? SubShareholderId { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public long Shares { get; set; }

        public FormStatus FormStatus { get; set; }
        public string FormStatusComments { get; set; }

        public double OwnershipPercentage { get; set; }

        public bool IncludeInFormula { get; set; }
        public bool IsMainShareholder { get; set; }
    }

    public class CompanyShareholderArchive : BaseBaseEntity
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public Shareholder Shareholder { get; set; }
        public int ShareholderId { get; set; }

        public Shareholder SubShareholder { get; set; }
        public int? SubShareholderId { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public long Shares { get; set; }

        public FormStatus FormStatus { get; set; }
        public string FormStatusComments { get; set; }

        public double OwnershipPercentage { get; set; }

    }
}
