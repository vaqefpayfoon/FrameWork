using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KavoshFrameWorkCore.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.FluentApiMapping
{
    public class CompanyAnnualMeetingMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<CompanyAnnualMeeting>
    {
        public override void Configure(EntityTypeBuilder<CompanyAnnualMeeting> t)
        {
            t.ToTable("CompanyAnnualMeetings");
            t.HasKey(x => x.Id);
            
            t.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.SubGroup).WithMany().HasForeignKey(x => x.SubGroupId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.MainGroup).WithMany().HasForeignKey(x => x.MainGroupId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.FiscalYear).WithMany().HasForeignKey(x => x.FiscalYearId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.Newspaper).WithMany().HasForeignKey(x => x.NewspaperId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.PrimaryAuditor).WithMany().HasForeignKey(x => x.PrimaryAuditorId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.SecondaryAuditor).WithMany().HasForeignKey(x=>x.SecondaryAuditorId).OnDelete( DeleteBehavior.Restrict);

        }
    }

}
