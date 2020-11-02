using KavoshFrameWorkCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KavoshFrameWorkCore.FluentApiMapping
{
    public class RoleFormActionAssignmentMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<RoleFormActionAssignment>
    {
        public override void Configure(EntityTypeBuilder<RoleFormActionAssignment> t)
        {
            t.ToTable("RoleFormActionAssignments");
            t.HasKey(x => x.Id);
            t.HasOne(x => x.SystemForm).WithMany().HasForeignKey(x => x.SystemFormId).OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class SystemFormMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<SystemForm>
    {
        public override void Configure(EntityTypeBuilder<SystemForm> t)
        {
            t.ToTable("SystemForms");
            t.Property(x => x.Priority);
            t.HasKey(x => x.Id);
        }
    }
}
