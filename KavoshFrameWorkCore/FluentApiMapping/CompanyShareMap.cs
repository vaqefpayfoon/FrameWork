using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KavoshFrameWorkCore.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.FluentApiMapping
{
    public class CompanyShareMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<CompanyShare>
    {
        public override void Configure(EntityTypeBuilder<CompanyShare> t)
        {
            t.ToTable("CompanyShares");
            t.HasKey(x => x.Id);
            
            t.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict);
        }
    }

}
