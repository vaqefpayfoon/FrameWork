using System;
using System.ComponentModel.DataAnnotations;

namespace KavoshFrameWorkCore.Models
{
    public interface IAdminEntity { }
    public class RoleFormActionAssignment : BaseBaseEntity, IAdminEntity
    {
        public string RoleId { get; set; }
        public ApplicationRole Role { get; set; }
        public SystemAction SystemAction { get; set; }
        public SystemForm SystemForm { get; set; }
        public int SystemFormId { get; set; }
    }

    public class SystemForm : BaseEntity, IAdminEntity
    {
        public string ParentTitle { get; set; }
        public string EntityName { get; set; }
        public bool ListOnly { get; set; }
        public int Priority { get; set; }

    }

    [Flags]
    public enum SystemAction
    {
        [Display(Name = "بدون عملیات")]
        None = 0,

        [Display(Name = "ایجاد")]
        Create = 1,

        [Display(Name = "مشاهده")]
        Read = 2,

        [Display(Name = "ویرایش")]
        Update = 4,

        [Display(Name = "حذف")]
        Delete = 8
    }
}
