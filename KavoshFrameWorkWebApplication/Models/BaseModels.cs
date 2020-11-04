using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KavoshFrameWorkWebApplication.Models
{
    public class KeyTitleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsSelected { get; set; }
    }

    public class KeyTextViewModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool IsSelected { get; set; }
    }

    public class BaseBaseViewModel
    {
        public int Id { get; set; }
        public DateTime? AddedDate { get; set; }
        public string SystemUserId { get; set; } 
      
    }


    public class BaseViewModel: BaseBaseViewModel
    {
        [Display(Name = nameof(Title), ResourceType = typeof(Resources.Labels))]        
        public string Title { get; set; }
        public SystemAction SystemAction { get; set; }
    }

    public class BaseStatusViewModel : BaseBaseViewModel
    {
        [Display(Name = nameof(FormStatus), ResourceType = typeof(Resources.Labels))]
        public FormStatus FormStatus { get; set; }

        [Display(Name = nameof(FormStatusComments), ResourceType = typeof(Resources.Labels))]
        public string FormStatusComments { get; set; }


        [Display(Name = nameof(CompanyTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? CompanyId { get; set; }

        [Display(Name = nameof(CompanyTitle), ResourceType = typeof(Resources.Labels))]
        public string CompanyTitle { get; set; }
        public DateTime? FromDate { get; set; }
        public string JalaliFromDate { get; set; }
        [Display(Name = nameof(ToDate), ResourceType = typeof(Resources.Labels))]
        public DateTime? ToDate { get; set; }
        public string JalaliToDate { get; set; }
    }

    public class KeyValueViewModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class ReportViewModel
    {
        public int Year { get; set; }
        public string Month { get; set; }
    }
    public class MartisViewModel
    {
        public MartisViewModel()
        {
            MatrisDetails = new List<MatrisDetailViewModel>();
        }
        public int FromRow { get; set; }
        public int ToRow { get; set; }
        public int FromColumn { get; set; }
        public int ToColumn { get; set; }
        public int SheetNo { get; set; }
        public List<MatrisDetailViewModel> MatrisDetails { get; set; }
    }
    public class MatrisDetailViewModel
    {
        public string Text { get; set; }
        public int RowNum { get; set; }
    }
}
