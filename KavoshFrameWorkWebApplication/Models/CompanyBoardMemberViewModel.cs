using Microsoft.AspNetCore.Mvc;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Controllers;
using KavoshFrameWorkWebApplication.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class CompanyBoardMemberViewModel : BaseStatusViewModel
    {

        [Display(Name = nameof(BoardofDirectorsLegalMemberTitle), ResourceType = typeof(Resources.Labels))]
        public string BoardofDirectorsLegalMemberTitle { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = nameof(BoardofDirectorsLegalMemberTitle), ResourceType = typeof(Resources.Labels))]
        public int BoardofDirectorsLegalMemberId { get; set; }

        
        [Display(Name = nameof(AgentTitle), ResourceType = typeof(Resources.Labels))]
        public string AgentTitle { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = nameof(AgentTitle), ResourceType = typeof(Resources.Labels))]
        public int AgentId { get; set; }
        
        [Display(Name = nameof(AppointmentDate), ResourceType = typeof(Resources.Labels))]
        public DateTime? AppointmentDate { get; set; }
        public string JalaliAppointmentDate { get; set; }
        [Display(Name = nameof(CompletionDate), ResourceType = typeof(Resources.Labels))]
        public DateTime? CompletionDate { get; set; }
        public string JalaliCompletionDate { get; set; }

        [Display(Name = nameof(BirthDate), ResourceType = typeof(Resources.Labels))]
        public DateTime? BirthDate { get; set; }

        [Display(Name = nameof(BirthDate), ResourceType = typeof(Resources.Labels))]
        public string JalaliBirthDate { get; set; }

        [Display(Name = nameof(ResignationDate), ResourceType = typeof(Resources.Labels))]
        public DateTime? ResignationDate { get; set; }
        [Display(Name = nameof(ResignationDate), ResourceType = typeof(Resources.Labels))]
        public string JalaliResignationDate { get; set; }

        [Display(Name = nameof(VerdictDate), ResourceType = typeof(Resources.Labels))]
        public DateTime? VerdictDate { get; set; }
        [Display(Name = nameof(VerdictDate), ResourceType = typeof(Resources.Labels))]
        public string JalaliVerdictDate { get; set; }

        [Display(Name = nameof(NewspaperEntryDate), ResourceType = typeof(Resources.Labels))]
        public DateTime? NewspaperEntryDate { get; set; }
        [Display(Name = nameof(NewspaperEntryDate), ResourceType = typeof(Resources.Labels))]
        public string JalaliNewspaperEntryDate { get; set; }


        [Display(Name = nameof(LastElectionMeetingDate), ResourceType = typeof(Resources.Labels))]
        public DateTime? LastElectionMeetingDate { get; set; }

        [Display(Name = nameof(LastElectionMeetingDate), ResourceType = typeof(Resources.Labels))]
        public string JalaliLastElectionMeetingDate { get; set; }


        [Display(Name = nameof(IsActive), ResourceType = typeof(Resources.Labels))]
        public bool IsActive { get; set; }

        [Display(Name = nameof(AgentCompanyTitle), ResourceType = typeof(Resources.Labels))]
        public string AgentCompanyTitle { get; set; }
        [Display(Name = nameof(AgentCompanyTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? AgentCompanyId { get; set; }

        [Display(Name = nameof(LegalMemberTypeTitle), ResourceType = typeof(Resources.Labels))]
        public string LegalMemberTypeTitle { get; set; }
        [Display(Name = nameof(LegalMemberTypeTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? LegalMemberTypeId { get; set; }


        [Display(Name = nameof(EducationDegreeTitle), ResourceType = typeof(Resources.Labels))]
        public string EducationDegreeTitle { get; set; }
        [Display(Name = nameof(EducationDegreeTitle), ResourceType = typeof(Resources.Labels))]
        public int? EducationDegreeId { get; set; }

        [Display(Name = nameof(OrganizationalPositionTitle), ResourceType = typeof(Resources.Labels))]
        public string OrganizationalPositionTitle { get; set; }
        [Display(Name = nameof(OrganizationalPositionTitle), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Messages))]
        public int? OrganizationalPositionId { get; set; }


        [Display(Name = nameof(Gender), ResourceType = typeof(Resources.Labels))]
        public Gender Gender { get; set; }
        [Display(Name = nameof(NationalCode), ResourceType = typeof(Resources.Labels))]
        public string NationalCode { get; set; }

    }
    public class CompanyBoardMemberExViewModel : BaseStatusViewModel
    {
        public string BoardofDirectorsLegalMemberTitleOld { get; set; }
        public string AgentTitleOld { get; set; }
        public string BoardofDirectorsLegalMemberTitle { get; set; }
        public string AgentTitle { get; set; }        
        public DateTime? AppointmentDate { get; set; }
    }
}
