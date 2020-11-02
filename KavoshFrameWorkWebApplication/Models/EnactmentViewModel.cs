using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace KavoshFrameWorkWebApplication.Models
{
    public class EnactmentViewModel : BaseStatusViewModel
    {
        public EnactmentViewModel()
        {
            //EnactmentDetails = new List<EnactmentDetail>
            //{
            //    new EnactmentDetail
            //    {
            //        EnactmentTitle = "متن مصوبه"
            //    }
            //};
        }
        [Display(Name = nameof(Title), ResourceType = typeof(Resources.Labels))]
        public string Title { get; set; }

        [Display(Name = nameof(Date), ResourceType = typeof(Resources.Labels))]
        public DateTime Date { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string JalaliDate { get; set; }
        public int? DetailId { get; set; }
        public string EnactmentTitle { get; set; }

        [Display(Name = nameof(EnactmentDetails), ResourceType = typeof(Resources.Labels))]     
        public List<EnactmentDetail> EnactmentDetails { get; set; }
    }
    public class EnactmentSearchViewModel
    {
        public string Search { get; set; }
        public List<EnactmentDetail> EnactmentDetails { get; set; }
    }
}
