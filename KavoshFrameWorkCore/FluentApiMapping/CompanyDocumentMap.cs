using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KavoshFrameWorkCore.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.FluentApiMapping
{
    public class CompanyDocumentMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<CompanyDocument>
    {
        public override void Configure(EntityTypeBuilder<CompanyDocument> t)
        {
            t.ToTable("CompanyDocuments");
            t.HasKey(x => x.Id);
   
            t.HasOne(x => x.SubGroup).WithMany().HasForeignKey(x => x.SubGroupId).OnDelete(DeleteBehavior.Restrict); 
            t.HasOne(x => x.MainGroup).WithMany().HasForeignKey(x => x.MainGroupId).OnDelete(DeleteBehavior.Restrict); 
            t.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.FiscalYear).WithMany().HasForeignKey(x => x.FiscalYearId).OnDelete(DeleteBehavior.Restrict);
            

        }
    }

    public class CompanyDocumentMediaMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<CompanyDocumentMedia>
    {
        public override void Configure(EntityTypeBuilder<CompanyDocumentMedia> t)
        {
            t.ToTable("CompanyDocumentMedias");
            t.HasKey(x => x.Id);
            t.Property(x => x.AddedDate);
            t.Property(x => x.FileName);
            t.Property(x => x.MediaType);
            t.HasOne(x => x.Row);
        }
    }
}
