using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class SubGroupViewModel : BaseViewModel
    {

        [Display(Name = nameof(MainGroupTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? MainGroupId { get; set; }

        [Display(Name = nameof(MainGroupTitle), ResourceType = typeof(Resources.Labels))]
        public string MainGroupTitle { get; set; }


    }
}
