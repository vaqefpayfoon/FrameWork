using KavoshFrameWorkWebApplication.Resources;
using System.ComponentModel.DataAnnotations;

namespace KavoshFrameWorkWebApplication.Models
{
    public class ApplicationRoleViewModel
    {
        [Display(Name = nameof(Title), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string Title { get; set; }
        public string Id { get; set; }
    }
}
