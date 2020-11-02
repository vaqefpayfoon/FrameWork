using System;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.Models
{
    public class DomainSetting : BaseEntity, IAdminEntity, ITemporal
    {
        public int Status { get; set; }

        public int Type { get; set; }

        public string TypeName { get; set; }

        public string Server { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Description { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
