using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? AddedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string RoleId { get; set; }
        public ICollection<CompanyUser> Companies { get; set; }

    }
    public class ApplicationRole : IdentityRole
    {
        public ICollection<RoleFormActionAssignment> RoleFormActionAssignments { get; set; }
    }
}
