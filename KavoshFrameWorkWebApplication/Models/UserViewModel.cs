using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KavoshFrameWorkWebApplication.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            CompanyIds = new List<int>();
        }
        public string Id { get; set; }

        [Display(Name = nameof(UserName), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string UserName { get; set; }

        [Display(Name = nameof(Email), ResourceType = typeof(Resources.Labels))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = nameof(Mobile), ResourceType = typeof(Resources.Labels))]
        public string Mobile { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(Resources.Labels))]
        public string PhoneNumber { get; set; }

        [Display(Name = nameof(FirstName), ResourceType = typeof(Resources.Labels))]
        public string FirstName { get; set; }
        [Display(Name = nameof(LastName), ResourceType = typeof(Resources.Labels))]
        public string LastName { get; set; }

        [Display(Name = nameof(Password), ResourceType = typeof(Resources.Labels))]
        [StringLength(40,  ErrorMessage = "طول رمز عبور باید حداقل 6 حرف باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = nameof(ConfirmPassword), ResourceType = typeof(Resources.Labels))]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن برابر نیستند")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string ConfirmPassword { get; set; }

        [Display(Name = nameof(RoleId), ResourceType = typeof(Resources.Labels))]
        public string RoleId { get; set; }

        [Display(Name = nameof(CompanyId), ResourceType = typeof(Resources.Labels))]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        [Display(Name = "CompanyTitle", ResourceType = typeof(Resources.Labels))]
        public List<int> CompanyIds { get; set; }
    }
}
