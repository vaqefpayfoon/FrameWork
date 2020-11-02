using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static KavoshFrameWorkCore.Models.Enums;

namespace KavoshFrameWorkCore.Models
{
    [DisplayName("اعضای هیات مدیره")]
    public class CompanyBoardMember : BaseEntity
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public BoardofDirectorsLegalMember BoardofDirectorsLegalMember { get; set; }
        public int BoardofDirectorsLegalMemberId { get; set; }
        public EducationDegree EducationDegree { get; set; }
        public int? EducationDegreeId { get; set; }
        public Agent Agent { get; set; }
        public int? AgentId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? LastElectionMeetingDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? ResignationDate { get; set; }
        public DateTime? VerdictDate { get; set; }
        public DateTime? NewspaperEntryDate { get; set; }
        public Gender Gender { get; set; }
        public bool IsActive { get; set; }
        public Company AgentCompany { get; set; }
        public int AgentCompanyId { get; set; }
        public LegalMemberType LegalMemberType { get; set; }
        public int LegalMemberTypeId { get; set; }
        public OrganizationalPosition OrganizationalPosition { get; set; }
        public int OrganizationalPositionId { get; set; }
        public FormStatus FormStatus { get; set; }
        public string FormStatusComments { get; set; }
        public string NationalCode { get; set; }
    }
    public enum Gender
    {
        Male, Female
    }
}
