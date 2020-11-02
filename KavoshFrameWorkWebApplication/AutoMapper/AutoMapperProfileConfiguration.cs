using AutoMapper;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Models;
namespace KavoshFrameWorkWebApplication.AutoMapper
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {

            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<UserViewModel, ApplicationUser>();

           
            CreateMap<SystemForm, SystemFormViewModel>();
            CreateMap<SystemFormViewModel, SystemForm>();

           
            CreateMap<ApplicationRole, BaseViewModel>().ReverseMap();

            CreateMap<SystemAction, BaseViewModel>().ReverseMap();
            CreateMap<SystemForm, BaseViewModel>().ReverseMap();

            CreateMap<RoleFormActionAssignment, RoleFormActionAssignmentViewModel>().ReverseMap();

            CreateMap<DomainSetting, DomainSettingViewModel>();
            CreateMap<DomainSettingViewModel, DomainSetting>();

            CreateMap<LogModel, LogViewModel>().ReverseMap();
            CreateMap<Company, CompanyViewModel>();
            CreateMap<CompanyViewModel, Company>();

            CreateMap<CompanyShare, CompanyShareViewModel>();
            CreateMap<CompanyShareViewModel, CompanyShare>();

            CreateMap<CompanyShareholder, CompanyShareholderViewModel>();
            CreateMap<CompanyShareholderViewModel, CompanyShareholder>();

            CreateMap<CompanyShareholderArchive, CompanyShareholderViewModel>();
            CreateMap<CompanyShareholderViewModel, CompanyShareholderArchive>();


            CreateMap<CompanyMeetingAssignment, CompanyMeetingAssignmentViewModel>();
            CreateMap<CompanyMeetingAssignmentViewModel, CompanyMeetingAssignment>();

            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<UserViewModel, ApplicationUser>();

            CreateMap<DomainSetting, DomainSettingViewModel>();
            CreateMap<DomainSettingViewModel, DomainSetting>();


            CreateMap<CompanyExtraordinaryMeeting, CompanyExtraordinaryMeetingViewModel>();
            CreateMap<CompanyExtraordinaryMeetingViewModel, CompanyExtraordinaryMeeting>();

            CreateMap<CompanyAnnualMeeting, CompanyAnnualMeetingViewModel>();
            CreateMap<CompanyAnnualMeetingViewModel, CompanyAnnualMeeting>();

            CreateMap<CompanyDocument, CompanyDocumentViewModel>();
            CreateMap<CompanyDocumentViewModel, CompanyDocument>();

            CreateMap<Portfo, PortfoViewModel>();
            CreateMap<PortfoViewModel, Portfo>();

            CreateMap<PortfoShareholderPair, PortfoShareholderPairViewModel>();
            CreateMap<PortfoShareholderPairViewModel, PortfoShareholderPair>();

            CreateMap<CompanyBoardMember, CompanyBoardMemberViewModel>();
            CreateMap<CompanyBoardMemberViewModel, CompanyBoardMember>();

            CreateMap<FiscalYear, FiscalYearViewModel>();
            CreateMap<FiscalYearViewModel, FiscalYear>();

            CreateMap<SubGroup, SubGroupViewModel>();
            CreateMap<SubGroupViewModel, SubGroup>();

            CreateMap<Enactment, EnactmentViewModel>();
            CreateMap<EnactmentViewModel, Enactment>();

            CreateMap<EnactmentDetail, EnactmentDetailViewModel>();
            CreateMap<EnactmentDetailViewModel, EnactmentDetail>();

            CreateMap<CompanyType, BaseViewModel>().ReverseMap();
            CreateMap<OrganizationalPosition, BaseViewModel>().ReverseMap();
            CreateMap<BoardofDirectorsLegalMember, BaseViewModel>().ReverseMap();
            CreateMap<Shareholder, BaseViewModel>().ReverseMap();
            CreateMap<LegalMemberType, BaseViewModel>().ReverseMap();
            CreateMap<CompanyPartnershipType, BaseViewModel>().ReverseMap();
            CreateMap<MainGroup, BaseViewModel>().ReverseMap();
            CreateMap<Newspaper, BaseViewModel>().ReverseMap();
            CreateMap<Auditor, BaseViewModel>().ReverseMap();
            CreateMap<Agent, BaseViewModel>().ReverseMap();
            CreateMap<CompanyActivityType, BaseViewModel>().ReverseMap();
            CreateMap<EducationDegree, BaseViewModel>().ReverseMap();
        }
    }
}
