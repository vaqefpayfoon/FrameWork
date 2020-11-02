using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KavoshFrameWorkCore.Models
{
    [DisplayName("شرکت ها")]
    public class Company : BaseEntity
    {
        public List<CompanyMedia> Medias { get; set; }
        public CompanyType CompanyType { get; set; }
        public int CompanyTypeId { get; set; }

        public CompanyActivityType CompanyActivityType { get; set; }
        public int? CompanyActivityTypeId { get; set; }

        public CompanyPartnershipType CompanyPartnershipType { get; set; }
        public int CompanyPartnershipTypeId { get; set; }

        public DateTime? EstablishmentDate { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public string NationalID { get; set; }
        public string EconomicCode { get; set; }
        public string RegistrationNumber { get; set; }
        public string RegistrationUnit { get; set; }
        public string OperationLicenseNumber { get; set; }
        public string CommercialCard { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public ICollection<CompanyUser> Users { get; set; }
        public int? NumberOfCompanyBoardMembers { get; set; }

        public Shareholder Shareholder { get; set; }
        public int? ShareholderId { get; set; }
    }

    public class CompanyUser:BaseBaseEntity
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
