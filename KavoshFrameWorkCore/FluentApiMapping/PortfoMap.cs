using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KavoshFrameWorkCore.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.FluentApiMapping
{
    public class PortfoMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<Portfo>
    {
        public override void Configure(EntityTypeBuilder<Portfo> t)
        {
            t.ToTable("Portfos");
            t.HasKey(x => x.Id);
            t.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict);
            //t.Property(x => x.AddedDate).IsRequired(false);
            //t.Property(x => x.AgentsNumber).IsRequired(false);
            //t.Property(x => x.AggregatePercentage).IsRequired(false);
            //t.Property(x => x.AggregatePercentageCalculationType).IsRequired(false);
            //t.Property(x => x.DeleteDate).IsRequired(false);
            //t.Property(x => x.IsDeleted).IsRequired(false);
            //t.Property(x => x.LastModifiedDate).IsRequired(false);
            //t.Property(x => x.ShareholderHoldingCommunication).IsRequired(false);
            //t.Property(x => x.SystemUserId).IsRequired(false);
            //t.Property(x => x.UnderManagementHoldingCommunication).IsRequired(false);
            
        }
    }
    public class PortfoShareholderPairMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<PortfoShareholderPair>
    {
        public override void Configure(EntityTypeBuilder<PortfoShareholderPair> t)
        {
            t.ToTable("PortfoShareholderPairs");
            t.HasKey(x => x.Id);

            t.HasOne(x => x.Portfo).WithMany(x=>x.PortfoShareholderPairs).HasForeignKey(x => x.PortfoId).OnDelete(DeleteBehavior.Restrict);

            t.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.Shareholder).WithMany().HasForeignKey(x => x.ShareholderId).OnDelete(DeleteBehavior.Restrict);
        }
    }
    
}
