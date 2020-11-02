using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KavoshFrameWorkCore.Models
{
    public class BaseTree
    {
        [StringLength(50)]
        public string Id { get; set; }
        [StringLength(250)]
        public string Text { get; set; }
        [StringLength(50)]
        public string ParentId { get; set; }
        public bool Opened { get; set; } = false;
        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? AddedDate { get; set; }
        public string SystemUserId { get; set; }
    }
}
