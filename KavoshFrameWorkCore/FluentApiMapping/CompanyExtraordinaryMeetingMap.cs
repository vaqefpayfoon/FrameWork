using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KavoshFrameWorkCore.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.FluentApiMapping
{
    public class CompanyExtraordinaryMeetingMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<CompanyExtraordinaryMeeting>
    {
        public override void Configure(EntityTypeBuilder<CompanyExtraordinaryMeeting> t)
        {
            t.ToTable("CompanyExtraordinaryMeetings");
            t.HasKey(x => x.Id);
            
            t.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.SubGroup).WithMany().HasForeignKey(x => x.SubGroupId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.MainGroup).WithMany().HasForeignKey(x => x.MainGroupId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.FiscalYear).WithMany().HasForeignKey(x => x.FiscalYearId).OnDelete(DeleteBehavior.Restrict);

        }
    }

    public class CompanyExtraordinaryMeetingMediaMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<CompanyExtraordinaryMeetingMedia>
    {
        public override void Configure(EntityTypeBuilder<CompanyExtraordinaryMeetingMedia> t)
        {
            t.ToTable("CompanyExtraordinaryMeetingMedias");
            t.HasKey(x => x.Id);
            t.Property(x => x.AddedDate);
            t.Property(x => x.FileName);
            t.Property(x => x.MediaType);
            t.HasOne(x => x.Row);
        }
    }

}
