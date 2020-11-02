using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class DashboardViewModel
    {
        public int PendingShareholders { get;  set; }
        public int PendingShares { get;  set; }
        public int PendingBoardMembers { get;  set; }
        public int PendingMeetingAssignments { get;  set; }
        public int PendingDocuments { get; set; }

        public int RejectedShareholders { get; internal set; }
        public int RejectedShares { get; internal set; }
        public int RejectedBoardMembers { get; internal set; }
        public int RejectedMeetingAssignments { get; internal set; }
        public int RejectedDocuments { get; internal set; }

        public int VerifiedShareholders { get; internal set; }
        public int VerifiedShares { get; internal set; }
        public int VerifiedBoardMembers { get; internal set; }
        public int VerifiedMeetingAssignments { get; internal set; }
        public int VerifiedDocuments { get; internal set; }

    }
}
