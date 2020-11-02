using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Resources;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KavoshFrameWorkWebApplication.Models
{
    public class DomainUserViewModel : ApplicationUser
    {

        public string dcString { get; set; }
        public string UserID { get; set; }
        public int Status { get; set; }
        public string UserName { get; set; }
        public string GroupName { get; set; }
        public string Email { get; set; }
        [JsonProperty("name")]
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonProperty("fullName")]
        public string FullName { get; set; }
        public int DomainID { get; set; }
        public string DomainName { get; set; }
        [JsonProperty("transfer")]
        public bool Transfer { get; set; }

        [Display(Name = nameof(RoleId), ResourceType = typeof(Resources.Labels))]
        public string RoleId { get; set; }

        [Display(Name = nameof(Password), ResourceType = typeof(Resources.Labels))]
        [StringLength(40, ErrorMessage = "طول رمز عبور باید حداقل 6 حرف باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Server { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = nameof(DomainId), ResourceType = typeof(Resources.Labels))]
        public int DomainId { get; set; }
        public bool IsSuccess { get; set; }
        public bool flag { get; set; }
        public string message { get; set; }
    }
}
