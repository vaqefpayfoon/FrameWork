using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class CompanyViewModel:BaseViewModel
    {

        [Display(Name = nameof(Address), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string Address { get; set; }
        [Display(Name = nameof(CompanyTypeTitle), ResourceType = typeof(Resources.Labels))]
        public string CompanyTypeTitle { get; set; }
        [Display(Name = nameof(CompanyTypeTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? CompanyTypeId { get; set; }

        [Display(Name = nameof(CompanyActivityTypeTitle), ResourceType = typeof(Resources.Labels))]
        public string CompanyActivityTypeTitle { get; set; }

        [Display(Name = nameof(CompanyActivityTypeTitle), ResourceType = typeof(Resources.Labels))]
        public int? CompanyActivityTypeId { get; set; }

        [Display(Name = nameof(CompanyPartnershipTypeTitle), ResourceType = typeof(Resources.Labels))]
        public string CompanyPartnershipTypeTitle { get; set; }

        [Display(Name = nameof(CompanyPartnershipTypeTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? CompanyPartnershipTypeId { get; set; }

        [Display(Name = nameof(EstablishmentDate), ResourceType = typeof(Resources.Labels))]
        public DateTime? EstablishmentDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string JalaliEstablishmentDate { get; set; }

        [Display(Name = nameof(RegistrationDate), ResourceType = typeof(Resources.Labels))]
        public DateTime? RegistrationDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string JalaliRegistrationDate { get; set; }


        [Display(Name = nameof(NationalID), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string NationalID { get; set; }

        [Display(Name = nameof(EconomicCode), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string EconomicCode { get; set; }

        [Display(Name = nameof(RegistrationNumber), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string RegistrationNumber { get; set; }

        [Display(Name = nameof(RegistrationUnit), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string RegistrationUnit { get; set; }

        [Display(Name = nameof(OperationLicenseNumber), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string OperationLicenseNumber { get; set; }

        [Display(Name = nameof(CommercialCard), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string CommercialCard { get; set; }

        [Display(Name = nameof(PostalCode), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public string PostalCode { get; set; }

        [Display(Name = nameof(NumberOfCompanyBoardMembers), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? NumberOfCompanyBoardMembers { get; set; }

        [Display(Name = "ShareholderTitle", ResourceType = typeof(Resources.Labels))]
        public int? ShareholderId { get; set; }
    }
}
