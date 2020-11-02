using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static KavoshFrameWorkCore.Models.Enums;

namespace KavoshFrameWorkCore.Models
{
    [DisplayName("تکالیف مجمع")]
    public class CompanyMeetingAssignment : BaseBaseEntity
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public string Assignments { get; set; }
        public string Report { get; set; }
        public string Actions { get; set; }


        public DateTime Date { get; set; }

        public FormStatus FormStatus { get; set; }
        public string FormStatusComments { get; set; }

    }
}
