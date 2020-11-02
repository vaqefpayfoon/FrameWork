using KavoshFrameWorkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{

    public class SingleMediaViewModel
    {
        public object ItemId { get; set; }
        public string EntityTitle { get; set; }
        public int MediaTypeId { get; set; }
        public string MediaTypeTitle { get; set; }
    }

    public class MediaUpdateViewModel
    {
        public int ItemId { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
        public string FileName { get; set; }
        public string MediaType { get; set; }
        public string Entity { get; set; }
    }

}
