using KavoshFrameWorkCore.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KavoshFrameWorkWebApplication.Models
{

    public class RoleFormActionAssignmentsViewModel
    {
        [Required]
        [Display(Name = nameof(RoleId), ResourceType = typeof(Resources.Labels))]
        public string RoleId { get; set; }
        public List<RoleFormActionAssignmentViewModel> Items { get; set; }
    }

    public class RoleFormActionAssignmentViewModel : BaseBaseViewModel
    {
        public SystemFormViewModel SystemForm { get; set; }
        public int SystemFormId { get; set; }

        //[Display(Name = nameof(SystemAction), ResourceType = typeof(Resources.Labels))]
        public SystemAction SystemAction { get; set; }
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public string SystemFormTitle { get; set; }
    }

    public class SystemFormViewModel : BaseViewModel
    {

        [Display(Name = nameof(ParentTitle), ResourceType = typeof(Resources.Labels))]
        public string ParentTitle { get; set; }

        [Display(Name = nameof(EntityName), ResourceType = typeof(Resources.Labels))]
        public string EntityName { get; set; }

        [Display(Name = nameof(ListOnly), ResourceType = typeof(Resources.Labels))]
        public bool ListOnly { get; set; }

        [Display(Name = nameof(Priority), ResourceType = typeof(Resources.Labels))]
        public int Priority { get; set; }
    }

    
}

