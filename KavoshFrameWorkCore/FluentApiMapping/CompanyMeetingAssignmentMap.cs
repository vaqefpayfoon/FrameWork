using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KavoshFrameWorkCore.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.FluentApiMapping
{
    public class CompanyMeetingAssignmentMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<CompanyMeetingAssignment>
    {
        public override void Configure(EntityTypeBuilder<CompanyMeetingAssignment> t)
        {
            t.ToTable("CompanyMeetingAssignments");
            t.HasKey(x => x.Id);
            
            t.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict); 
        }
    }

    public class CompanyMeetingAssignmentMediaMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<CompanyMeetingAssignmentMedia>
    {
        public override void Configure(EntityTypeBuilder<CompanyMeetingAssignmentMedia> t)
        {
            t.ToTable("CompanyMeetingAssignmentMedias");
            t.HasKey(x => x.Id);
            t.Property(x => x.AddedDate);
            t.Property(x => x.FileName);
            t.Property(x => x.MediaType);
            t.HasOne(x => x.Row);
        }
    }

}
