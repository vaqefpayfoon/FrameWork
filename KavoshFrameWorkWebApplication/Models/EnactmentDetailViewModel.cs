using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class EnactmentDetailViewModel : BaseStatusViewModel
    {
        public Enactment Enactment { get; set; }
        public int EnactmentId { get; set; }
        public string EnactmentTitle { get; set; }
    }
}
