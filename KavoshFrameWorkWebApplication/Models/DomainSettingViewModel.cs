using KavoshFrameWorkWebApplication.Resources;
using System.ComponentModel.DataAnnotations;

namespace KavoshFrameWorkWebApplication.Models
{
    public class DomainSettingViewModel : BaseViewModel
    {
        public int? Status { get; set; }

        // public int DomainID { get; set; }
        public string Title { get; set; }

        public int? Type { get; set; }


        public string TypeName { get; set; }


        public string Server { get; set; }

        [Display(Name = nameof(UserName), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string UserName { get; set; }

        [Display(Name = nameof(Password), ResourceType = typeof(Resources.Labels))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Description { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
