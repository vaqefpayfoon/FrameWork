using AutoMapper;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static KavoshFrameWorkWebApplication.Helpers.Utility;

namespace KavoshFrameWorkWebApplication.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public UserManager<ApplicationUser> _userManager;
        public IGenericRepository<CompanyUser> _companyUserRepository;
        public List<int> CompanyIds { get; set; }
        public List<int> DomainIds { get; set; }
        public string UserId { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }

        public bool HasAccess(int? companyId)
        {
            if (!companyId.HasValue)
                return false;
            return User.IsInRole("Admin") || CompanyIds.Contains(companyId.Value);
        }
        public bool HasAccess1(int? domainID)
        {
            if (!domainID.HasValue)
                return false;
            return User.IsInRole("Admin") || DomainIds.Contains(domainID.Value);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_companyUserRepository != null)
            {
                CompanyIds = _companyUserRepository.GetAsQueryable(includeProperties: "User")
               .Where(x => x.User.UserName == User.Identity.Name)
               .Select(x => x.CompanyId).ToList();
            }
            UserId = User.FindFirst(x => x.Type == "sub" || x.Type == ClaimTypes.NameIdentifier)?.Value;

            base.OnActionExecuting(context);
        }

    }

    public class CompanyTypeController : BaseGenericController<CompanyType, BaseViewModel>
    {
        public CompanyTypeController(IGenericRepository<CompanyType> repository, IMapper mapper, IEntityService entityService) : base(repository, mapper, entityService) { }
    }
    public class OrganizationalPositionController : BaseGenericController<OrganizationalPosition, BaseViewModel>
    {
        public OrganizationalPositionController(IGenericRepository<OrganizationalPosition> repository, IMapper mapper, IEntityService entityService) : base(repository, mapper, entityService) { }
    }

    public class BoardofDirectorsLegalMemberController : BaseGenericController<BoardofDirectorsLegalMember, BaseViewModel>
    {
        public BoardofDirectorsLegalMemberController(IGenericRepository<BoardofDirectorsLegalMember> repository, IMapper mapper, IEntityService entityService) : base(repository, mapper, entityService) { }
    }
    public class ShareholderController : BaseGenericController<Shareholder, BaseViewModel>
    {
        public ShareholderController(IGenericRepository<Shareholder> repository, IMapper mapper, IEntityService entityService) : base(repository, mapper, entityService) { }
    }
    public class LegalMemberTypeController : BaseGenericController<LegalMemberType, BaseViewModel>
    {
        public LegalMemberTypeController(IGenericRepository<LegalMemberType> repository, IMapper mapper, IEntityService entityService) : base(repository, mapper, entityService) { }
    }
    public class CompanyPartnershipTypeController : BaseGenericController<CompanyPartnershipType, BaseViewModel>
    {
        public CompanyPartnershipTypeController(IGenericRepository<CompanyPartnershipType> repository, IMapper mapper, IEntityService entityService) : base(repository, mapper, entityService) { }
    }
    public class MainGroupController : BaseGenericController<MainGroup, BaseViewModel>
    {
        public MainGroupController(IGenericRepository<MainGroup> repository, IMapper mapper, IEntityService entityService) : base(repository, mapper, entityService) { }
    }

    public class NewspaperController : BaseGenericController<Newspaper, BaseViewModel>
    {
        public NewspaperController(IGenericRepository<Newspaper> repository, IMapper mapper, IEntityService entityService) : base(repository, mapper, entityService) { }
    }


    public class AuditorController : BaseGenericController<Auditor, BaseViewModel>
    {
        public AuditorController(IGenericRepository<Auditor> repository, IMapper mapper, IEntityService entityService) : base(repository, mapper, entityService) { }
    }

    public class AgentController : BaseGenericController<Agent, BaseViewModel>
    {
        public AgentController(IGenericRepository<Agent> repository, IMapper mapper, IEntityService entityService) : base(repository, mapper, entityService) { }
    }

    public class CompanyActivityTypeController : BaseGenericController<CompanyActivityType, BaseViewModel>
    {
        public CompanyActivityTypeController(IGenericRepository<CompanyActivityType> repository, IMapper mapper, IEntityService entityService) : base(repository, mapper, entityService) { }
    }

    public class EducationDegreeController : BaseGenericController<EducationDegree, BaseViewModel>
    {
        public EducationDegreeController(IGenericRepository<EducationDegree> repository, IMapper mapper, IEntityService entityService) : base(repository, mapper, entityService) { }
    }

}
