using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KavoshFrameWorkCore.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.FluentApiMapping
{
    public class EnactmentMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<Enactment>
    {
        public override void Configure(EntityTypeBuilder<Enactment> t)
        {
            t.ToTable("Enactments");
            t.HasKey(x => x.Id);
            t.Property(x => x.Title).IsRequired();
            t.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict);
        }
    }
    public class EnactmentDetailMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<EnactmentDetail>
    {
        public override void Configure(EntityTypeBuilder<EnactmentDetail> t)
        {
            t.ToTable("EnactmentDetails");
            t.HasKey(x => x.Id);
            t.Property(x => x.EnactmentTitle).IsRequired();
            t.HasOne(x => x.Enactment).WithMany(y => y.EnactmentDetails).HasForeignKey(x => x.EnactmentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
    public class EnactmentMediaMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<EnactmentMedia>
    {
        public override void Configure(EntityTypeBuilder<EnactmentMedia> t)
        {
            t.ToTable("EnactmentMedias");
            t.HasKey(x => x.Id);
            t.Property(x => x.AddedDate);
            t.Property(x => x.FileName);
            t.Property(x => x.MediaType);
            t.HasOne(x => x.Row);
        }
    }
}
