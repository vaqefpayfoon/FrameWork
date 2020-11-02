
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class HtmlEditorViewModel
    {
        public object Id { get; set; }
        public string EntityName { get; set; }
        public string Selector { get; set; }
    }
}
