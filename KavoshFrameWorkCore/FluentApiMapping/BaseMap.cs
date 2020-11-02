using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KavoshFrameWorkCore.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.FluentApiMapping
{
   
    public class CompanyTypeMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<CompanyType>
    {
        public override void Configure(EntityTypeBuilder<CompanyType> t)
        {
            t.ToTable("CompanyTypes");
            t.HasKey(x => x.Id);
            t.Property(x => x.Title).IsRequired();
        }
    }

    public class OrganizationalPositionMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<OrganizationalPosition>
    {
        public override void Configure(EntityTypeBuilder<OrganizationalPosition> t)
        {
            t.ToTable("OrganizationalPositions");
            t.HasKey(x => x.Id);
            t.Property(x => x.Title).IsRequired();
        }
    }


    public class AgentMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<Agent>
    {
        public override void Configure(EntityTypeBuilder<Agent> t)
        {
            t.ToTable("Agents");
            t.HasKey(x => x.Id);
            t.Property(x => x.Title).IsRequired();
        }
    }
    public class BoardofDirectorsLegalMemberMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<BoardofDirectorsLegalMember>
    {
        public override void Configure(EntityTypeBuilder<BoardofDirectorsLegalMember> t)
        {
            t.ToTable("BoardofDirectorsLegalMembers");
            t.HasKey(x => x.Id);
            t.Property(x => x.Title).IsRequired();
        }
    }

    public class ShareholderMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<Shareholder>
    {
        public override void Configure(EntityTypeBuilder<Shareholder> t)
        {
            t.ToTable("Shareholders");
            t.HasKey(x => x.Id);
            t.Property(x => x.Title).IsRequired();
        }
    }

    public class AuditorMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<Auditor>
    {
        public override void Configure(EntityTypeBuilder<Auditor> t)
        {
            t.ToTable("Auditors");
            t.HasKey(x => x.Id);
            t.Property(x => x.Title).IsRequired();

        }
    }

    public class LegalMemberTypeMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<LegalMemberType>
    {
        public override void Configure(EntityTypeBuilder<LegalMemberType> t)
        {
            t.ToTable("LegalMemberTypes");
            t.HasKey(x => x.Id);
            t.Property(x => x.Title).IsRequired();
        }
    }

    public class CompanyPartnershipTypeMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<CompanyPartnershipType>
    {
        public override void Configure(EntityTypeBuilder<CompanyPartnershipType> t)
        {
            t.ToTable("CompanyPartnershipTypes");
            t.HasKey(x => x.Id);
            t.Property(x => x.Title).IsRequired();
        }
    }

    public class MainGroupMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<MainGroup>
    {
        public override void Configure(EntityTypeBuilder<MainGroup> t)
        {
            t.ToTable("MainGroups");
            t.HasKey(x => x.Id);
            t.Property(x => x.Title).IsRequired();
        }
    }

    public class SubGroupMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<SubGroup>
    {
        public override void Configure(EntityTypeBuilder<SubGroup> t)
        {
            t.ToTable("SubGroups");
            t.HasKey(x => x.Id);
            t.Property(x => x.Title).IsRequired();
            t.HasOne(x => x.MainGroup);
        }
    }

    public class NewspaperMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<Newspaper>
    {
        public override void Configure(EntityTypeBuilder<Newspaper> t)
        {
            t.ToTable("Newspapers");
            t.HasKey(x => x.Id);
            t.Property(x => x.Title).IsRequired();
        }
    }

    public class FiscalYearMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<FiscalYear>
    {
        public override void Configure(EntityTypeBuilder<FiscalYear> t)
        {
            t.ToTable("FiscalYears");
            t.HasKey(x => x.Id);
            t.Property(x => x.Title).IsRequired();
            t.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict);

        }
    }

    public class CompanyActivityTypeMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<CompanyActivityType>
    {
        public override void Configure(EntityTypeBuilder<CompanyActivityType> t)
        {
            t.ToTable("CompanyActivityTypes");
            t.HasKey(x => x.Id);
            t.Property(x => x.Title).IsRequired();

        }
    }
    public class EducationDegreeMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<EducationDegree>
    {
        public override void Configure(EntityTypeBuilder<EducationDegree> t)
        {
            t.ToTable("EducationDegrees");
            t.HasKey(x => x.Id);
            t.Property(x => x.Title).IsRequired();

        }
    }
}
