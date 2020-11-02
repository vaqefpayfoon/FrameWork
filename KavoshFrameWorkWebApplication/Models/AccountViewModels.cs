using KavoshFrameWorkWebApplication.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = nameof(UserName), ResourceType = typeof(Resources.Labels))]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = nameof(Password), ResourceType = typeof(Resources.Labels))]
        public string Password { get; set; }

        [Display(Name = nameof(RememberMe), ResourceType = typeof(Resources.Labels))]
        public bool RememberMe { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = nameof(DomainId), ResourceType = typeof(Resources.Labels))]
        public int DomainId { get; set; }
    }

    public class ChangePasswordViewModel
    {

        [DataType(DataType.Password)]
        [Display(Name = nameof(OldPassword), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [StringLength(40, ErrorMessage = "طول رمز عبور باید حداقل 6 حرف باشد", MinimumLength = 6)]
        [Display(Name = nameof(Password), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = nameof(ConfirmPassword), ResourceType = typeof(Resources.Labels))]
        [Compare("Password", ErrorMessage ="رمز عبور و تکرار آن برابر نیستند")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string ConfirmPassword { get; set; }

    }

    public class ResetPasswordViewModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.RequiredField))]
        [DataType(DataType.Password)]
        [Display(Name = nameof(NewPassword), ResourceType = typeof(Resources.Labels))]

        [StringLength(40, ErrorMessage = "طول رمز عبور باید حداقل 6 حرف باشد", MinimumLength = 6)]
        public string NewPassword { get; set; }

        public string Code { get; set; }
        
    }
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.RequiredField))]
        [Display(Name = nameof(Mobile), ResourceType = typeof(Resources.Labels))]

        public string Mobile { get; set; }
       
    }
}
