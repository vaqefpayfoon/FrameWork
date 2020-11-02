using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KavoshFrameWorkCore.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.FluentApiMapping
{
    public class CompanyShareholderMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<CompanyShareholder>
    {
        public override void Configure(EntityTypeBuilder<CompanyShareholder> t)
        {
            t.ToTable("CompanyShareholders");
            t.HasKey(x => x.Id);
            t.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.Shareholder).WithMany().HasForeignKey(x => x.ShareholderId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.SubShareholder).WithMany().HasForeignKey(x => x.SubShareholderId).OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class CompanyShareholderArchiveMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<CompanyShareholderArchive>
    {
        public override void Configure(EntityTypeBuilder<CompanyShareholderArchive> t)
        {
            t.ToTable("CompanyShareholderArchives");
            t.HasKey(x => x.Id);
            t.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.Shareholder).WithMany().HasForeignKey(x => x.ShareholderId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.SubShareholder).WithMany().HasForeignKey(x => x.SubShareholderId).OnDelete(DeleteBehavior.Restrict);
        }
    }

}
