using System.ComponentModel.DataAnnotations;

namespace KavoshFrameWorkWebApplication.Models
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
