using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static KavoshFrameWorkCore.Models.Enums;

namespace KavoshFrameWorkCore.Models
{
    [DisplayName("سرمایه شرکت")]
    public class CompanyShare : BaseBaseEntity
    {        
        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public DateTime Date { get; set; }
        public long PreviousCapital { get; set; }
        public long PreviousShares { get; set; }
        public long CurrentCapital { get; set; }
        public long CurrentShares { get; set; }

        public double CapitalIncreaseFromAccumulatedProfits { get; set; }
        public double CapitalIncreaseFromCash { get; set; }
        public double CapitalIncreaseFromShareholderReceivables { get; set; }
        public double CapitalIncreaseFromAssetsRevaluation { get; set; }

        public FormStatus FormStatus { get; set; }
        public string FormStatusComments { get; set; }


        public long ParValue { get; set; }
    }
}
