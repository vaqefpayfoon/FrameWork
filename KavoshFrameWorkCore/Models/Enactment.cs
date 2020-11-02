using System;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.Models
{
    public class Enactment : BaseEntity
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<EnactmentDetail> EnactmentDetails { get; set; }
    }
    public class EnactmentDetail : BaseBaseEntity
    {
        public Enactment Enactment { get; set; }
        public int EnactmentId { get; set; }
        public string EnactmentTitle { get; set; }
    }
}
