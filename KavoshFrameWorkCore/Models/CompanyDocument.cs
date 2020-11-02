using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace KavoshFrameWorkCore.Models
{
    [DisplayName("اطلاعیه")]
    public class CompanyDocument : BaseEntity
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public MainGroup MainGroup { get; set; }
        public int MainGroupId { get; set; }

        public SubGroup SubGroup { get; set; }
        public int SubGroupId { get; set; }
        public FiscalYear FiscalYear { get; set; }
        public int FiscalYearId { get; set; }
        public FormStatus FormStatus { get; set; }
        public string FormStatusComments { get; set; }


    }
}
