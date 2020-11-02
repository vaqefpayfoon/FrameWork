using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KavoshFrameWorkCore.Models;

namespace KavoshFrameWorkCore.Dto
{
    public class RoleFormActionAssignmentDto
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string ParentTitle { get; set; }
        public string Title { get; set; }

        public SystemAction SystemAction { get; set; }
        public int Priority { get; set; }
        public bool ListOnly { get; set; }
    }
}
