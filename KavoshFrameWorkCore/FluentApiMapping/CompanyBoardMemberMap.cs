using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KavoshFrameWorkCore.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.FluentApiMapping
{
    public class CompanyBoardMemberMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<CompanyBoardMember>
    {
        public override void Configure(EntityTypeBuilder<CompanyBoardMember> t)
        {
            t.ToTable("CompanyBoardMembers");
            t.HasKey(x => x.Id);
           
            t.HasOne(x => x.BoardofDirectorsLegalMember).WithMany().HasForeignKey(x => x.BoardofDirectorsLegalMemberId).OnDelete(DeleteBehavior.Restrict); 
            t.HasOne(x => x.AgentCompany).WithMany().HasForeignKey(x => x.AgentCompanyId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.OrganizationalPosition).WithMany().HasForeignKey(x => x.OrganizationalPositionId).OnDelete(DeleteBehavior.Restrict); 
            t.HasOne(x => x.LegalMemberType).WithMany().HasForeignKey(x => x.LegalMemberTypeId).OnDelete(DeleteBehavior.Restrict); 
            t.HasOne(x => x.Company).WithMany().HasForeignKey(x=>x.CompanyId).OnDelete( DeleteBehavior.Restrict);
            t.HasOne(x => x.Agent).WithMany().HasForeignKey(x => x.AgentId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.EducationDegree).WithMany().HasForeignKey(x => x.EducationDegreeId).OnDelete(DeleteBehavior.Restrict);

        }
    }

    
}
