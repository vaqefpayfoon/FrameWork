using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KavoshFrameWorkCore.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.FluentApiMapping
{
    public class CompanyMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> t)
        {
            t.ToTable("Companies");
            t.HasKey(x => x.Id);
            t.Property(x => x.Address);
            t.Property(x => x.CommercialCard);
            t.Property(x => x.EconomicCode);
            t.Property(x => x.EstablishmentDate);
            t.Property(x => x.NationalID);
            t.Property(x => x.OperationLicenseNumber);
            t.Property(x => x.PostalCode);
            t.Property(x => x.RegistrationDate);
            t.Property(x => x.RegistrationNumber);
            t.Property(x => x.RegistrationUnit);
            t.Property(x => x.Title);

            t.HasOne(x => x.CompanyPartnershipType).WithMany().HasForeignKey(x => x.CompanyPartnershipTypeId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.CompanyType).WithMany().HasForeignKey(x => x.CompanyTypeId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.CompanyActivityType).WithMany().HasForeignKey(x => x.CompanyActivityTypeId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.Shareholder).WithMany().HasForeignKey(x => x.ShareholderId).OnDelete(DeleteBehavior.Restrict);

        }
    }

    public class CompanyUserMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<CompanyUser>
    {
        public override void Configure(EntityTypeBuilder<CompanyUser> t)
        {
            t.ToTable("CompanyUsers");
            t.HasKey(x => x.Id);
            t.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId);
            t.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }

    public class CompanyMediaMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<CompanyMedia>
    {
        public override void Configure(EntityTypeBuilder<CompanyMedia> t)
        {
            t.ToTable("CompanyMedias");
            t.HasKey(x => x.Id);
            t.Property(x => x.AddedDate);
            t.Property(x => x.FileName);
            t.Property(x => x.MediaType);
            t.HasOne(x => x.Row);
        }
    }
}
