using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public enum AdminPageType
    {
        List, Create, Edit
    }

    public enum RoleType
    {
        [Display(Name = "ادمین")]
        Admin,
        [Display(Name = "شرکت")]
        Company,
    }

    public enum FormStatus
    {
        [Display(Name = "بررسی نشده")]
        Pending = 0,
        [Display(Name = "تایید شده")]
        Verified = 1,
        [Display(Name = "رد شده")]
        Rejected = 2
    }

    public enum Month
    {
        [Display(Name = "فروردین")]
        Far = 1,
        [Display(Name = "اردیبهشت")]
        Ord = 2,
        [Display(Name = "خرداد")]
        Khor = 3,
        [Display(Name = "تیر")]
        Tir = 4,
        [Display(Name = "مرداد")]
        Mor = 5,
        [Display(Name = "شهریور")]
        Shah = 6,
        [Display(Name = "مهر")]
        Mehr = 7,
        [Display(Name = "آبان")]
        Aban = 8,
        [Display(Name = "آذر")]
        Azar = 9,
        [Display(Name = "دی")]
        Dey = 10,
        [Display(Name = "بهمن")]
        Bah = 11,
        [Display(Name = "اسفند")]
        Esf = 12,

    }
}
