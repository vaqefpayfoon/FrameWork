using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkCore.FluentApiMapping;
using KavoshFrameWorkCommon.Extensions;

namespace KavoshFrameWorkCore
{
    public class KavoshFrameWorkContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {        
        public KavoshFrameWorkContext(DbContextOptions<KavoshFrameWorkContext> option) : base(option) { }        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            AddConfigurations(builder);
        }
        public DbSet<LogModel> Logs { get; set; }
        public DbSet<DomainSetting> DomainSetting { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        void AddConfigurations(ModelBuilder builder)
        {
            builder.AddConfiguration(new SystemFormMap());
            builder.AddConfiguration(new RoleFormActionAssignmentMap());
            builder.AddConfiguration(new DomainSettingMap());
            builder.AddConfiguration(new CompanyMap());
            builder.AddConfiguration(new CompanyMediaMap());
            builder.AddConfiguration(new CompanyTypeMap());
            builder.AddConfiguration(new OrganizationalPositionMap());
            builder.AddConfiguration(new AgentMap());
            builder.AddConfiguration(new BoardofDirectorsLegalMemberMap());
            builder.AddConfiguration(new ShareholderMap());
            builder.AddConfiguration(new LegalMemberTypeMap());
            builder.AddConfiguration(new CompanyPartnershipTypeMap());
            builder.AddConfiguration(new AuditorMap());
            builder.AddConfiguration(new CompanyShareMap());
            builder.AddConfiguration(new CompanyShareholderMap());
            builder.AddConfiguration(new CompanyShareholderArchiveMap());
            builder.AddConfiguration(new CompanyMeetingAssignmentMap());
            builder.AddConfiguration(new MainGroupMap());
            builder.AddConfiguration(new SubGroupMap());
            builder.AddConfiguration(new NewspaperMap());
            builder.AddConfiguration(new FiscalYearMap());
            builder.AddConfiguration(new CompanyActivityTypeMap());
            builder.AddConfiguration(new EducationDegreeMap());
            builder.AddConfiguration(new CompanyExtraordinaryMeetingMap());
            builder.AddConfiguration(new CompanyAnnualMeetingMap());
            builder.AddConfiguration(new CompanyDocumentMap());
            builder.AddConfiguration(new CompanyDocumentMediaMap());
            builder.AddConfiguration(new CompanyBoardMemberMap());
            builder.AddConfiguration(new CompanyMeetingAssignmentMediaMap());
            builder.AddConfiguration(new CompanyExtraordinaryMeetingMediaMap());
            builder.AddConfiguration(new PortfoMap());
            builder.AddConfiguration(new PortfoShareholderPairMap());
            builder.AddConfiguration(new EnactmentMap());
            builder.AddConfiguration(new EnactmentMediaMap());
        }

    }
}
