using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KavoshFrameWorkCore.Models
{
    public interface IAccessible { }
    public interface ITemporal { }
    public class BaseBaseEntity : IAccessible
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? AddedDate { get; set; }
        public string SystemUserId { get; set; }
    }

    [NotMapped]
    public class BaseEntity : BaseBaseEntity
    {
        public string Title { get; set; }
    }
    [DisplayName("نوع شرکت")]
    public class CompanyType : BaseEntity { }
    [DisplayName("سمت سازمانی")]
    public class OrganizationalPosition : BaseEntity { }
    [DisplayName("اعضای هیات مدیره")]
    public class BoardofDirectorsLegalMember : BaseEntity { }
    [DisplayName("نماینده")]
    public class Agent : BaseEntity { }
    [DisplayName("سهامدار")]
    public class Shareholder : BaseEntity { }
    [DisplayName("نوع عضو حقوقی")]
    public class LegalMemberType : BaseEntity { }
    [DisplayName("نوع شرکت سهامی")]
    public class CompanyPartnershipType : BaseEntity { }
    [DisplayName("گروه اصلی")]
    public class MainGroup : BaseEntity { }
    [DisplayName("روزنامه رسمی")]
    public class Newspaper : BaseEntity { }
    [DisplayName("حسابرس")]
    public class Auditor : BaseEntity { }
    [DisplayName("نوع فعالیت شرکت")]
    public class CompanyActivityType : BaseEntity { }
    [DisplayName("مدرک تحصیلی")]
    public class EducationDegree : BaseEntity { }
    [DisplayName("زیر گروه")]
    public class SubGroup : BaseEntity
    {
        public MainGroup MainGroup { get; set; }
        public int? MainGroupId { get; set; }
    }
    [DisplayName("سال مالی")]
    public class FiscalYear : BaseEntity
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Company Company { get; set; }
        public int? CompanyId { get; set; }
    }

}
