using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KavoshFrameWorkCore.Models
{
    [NotMapped]
    public class Media : BaseEntity
    {
        public string FileName { get; set; }
        public string Description { get; set; }
        public string MediaType { get; set; }
        public int RowId { get; set; }
    }

    public class CompanyMedia : Media
    {
        public Company Row { get; set; }
    }

    public class CompanyDocumentMedia : Media
    {
        public CompanyDocument Row { get; set; }
    }

    public class CompanyMeetingAssignmentMedia : Media
    {
        public CompanyMeetingAssignment Row { get; set; }
    }

    public class CompanyExtraordinaryMeetingMedia : Media
    {
        public CompanyExtraordinaryMeeting Row { get; set; }
    }
    public class EnactmentMedia : Media
    {
        public Enactment Row { get; set; }
    }
}


