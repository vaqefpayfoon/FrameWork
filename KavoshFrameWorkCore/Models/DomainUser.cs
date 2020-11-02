using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace KavoshFrameWorkCore.Models
{
    
    public class DomainUser : ApplicationUser
    {
        public string dcString { get; set; }
        public string UserID { get; set; }
        public int Status { get; set; }
        public string GroupName { get; set; }
        public string DisplayName { get; set; }
        public string FullName { get; set; }
        public int DomainID { get; set; }
        public string DomainName { get; set; }
        public bool Transfer { get; set; }
    }
}
