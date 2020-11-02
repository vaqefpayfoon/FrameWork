using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KavoshFrameWorkCore.Models
{
    [DisplayName("مجامع عمومی فوق العاده")]
    public class CompanyExtraordinaryMeeting : BaseBaseEntity
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public MainGroup MainGroup { get; set; }
        public int MainGroupId { get; set; }
        public SubGroup SubGroup { get; set; }
        public int SubGroupId { get; set; }
        public FiscalYear FiscalYear { get; set; }
        public int FiscalYearId { get; set; }
        public string Decisions { get; set; }
        public DateTime Date { get; set; }
        public MeetingSubject MeetingSubject { get; set; }
        public AssemblyType AssemblyType { get; set; }
        public long CapitalChangeFrom { get; set; }
        public long CapitalChangeTo { get; set; }
    }

    public enum MeetingSubject
    {
        [Display(Name ="افزایش سرمایه")]
        CapitalIncrease,
        [Display(Name ="کاهش سرمایه")]
        CapitalReduction,
        [Display(Name = "تغییر اساسنامه")]
        StatuteChange,
        [Display(Name ="انحلال شرکت")]
        CompnayDissolution
    }
    public enum AssemblyType
    {
        [Display(Name = "مجمع عمومی فوق العاده")]
        CommonExtraordinaryAssembly,
        [Display(Name = "مجمع عمومی عادی سالانه")]
        CommonAnnualAssembly,
        [Display(Name = "مجمع عمومی عادی بطور فوق العاده")]
        CommonNormalExtraordinaryAssembly,
        [Display(Name = "مجمع عمومی موسس")]
        CommonInstitionAssembly
    }
}
