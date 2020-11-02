using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Models;
using KavoshFrameWorkCommon.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Serilog;

namespace KavoshFrameWorkWebApplication.Controllers
{

    public class CompanyBoardMemberController : BaseController
    {
        IMapper _mapper;
        private readonly IGenericRepository<Company> company;
        IGenericRepository<CompanyBoardMember> _companyBoardMemberRepository;
        IGenericRepository<LegalMemberType> _legalMemberTypeRepository;
        IGenericRepository<Agent> _agentRepository;
        IGenericRepository<BoardofDirectorsLegalMember> _boardofDirectorsLegalMemberRepository;
        IGenericRepository<OrganizationalPosition> _organizationalPositionRepository;
        private readonly IEntityService entityService;

        public CompanyBoardMemberController(IGenericRepository<CompanyBoardMember> companyBoardMemberRepository, IMapper mapper, UserManager<ApplicationUser> userManager,
        IGenericRepository<Company> company,
        IGenericRepository<CompanyUser> companyUserRepository,
        IGenericRepository<LegalMemberType> legalMemberTypeRepository,
        IGenericRepository<Agent> agentRepository,
        IGenericRepository<BoardofDirectorsLegalMember> boardofDirectorsLegalMemberRepository,
        IGenericRepository<OrganizationalPosition> organizationalPositionRepository, IEntityService _entityService
             )
        {
            _companyBoardMemberRepository = companyBoardMemberRepository;
            _mapper = mapper;
            this.company = company;
            this._userManager = userManager;
            this._companyUserRepository = companyUserRepository;
            _legalMemberTypeRepository = legalMemberTypeRepository;
            _agentRepository = agentRepository;
            _boardofDirectorsLegalMemberRepository = boardofDirectorsLegalMemberRepository;
            _organizationalPositionRepository = organizationalPositionRepository;
            entityService = _entityService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.SystemAction = await entityService.GetUserAllowedActionFor(User.Identity.Name, typeof(Company));
            if ((((SystemAction)ViewBag.SystemAction) & SystemAction.Update) == 0)
                return Forbid();


            List<CompanyBoardMember> items;

            if (User.IsInRole("Admin"))
                items = _companyBoardMemberRepository.GetAsQueryable(includeProperties: "BoardofDirectorsLegalMember,AgentCompany,LegalMemberType,OrganizationalPosition,Company,Agent").ToList();
            else
                items = items = _companyBoardMemberRepository.GetAsQueryable(x => CompanyIds.Contains(x.Id), includeProperties: "BoardofDirectorsLegalMember,AgentCompany,LegalMemberType,OrganizationalPosition,Company,Agent").ToList(); 

            var model = _mapper.Map<IEnumerable<CompanyBoardMember>, IEnumerable<CompanyBoardMemberViewModel>>(items);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CompanyBoardMemberViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyBoardMemberViewModel model)
        {
            model.SystemUserId = UserId;
     
            if (!HasAccess(model.CompanyId))
                return View("Error");

            //if (ModelState.IsValid)
            {
                model.AppointmentDate = model.JalaliAppointmentDate.ToDateTime();
                model.CompletionDate = model.JalaliCompletionDate.ToDateTime();
                model.NewspaperEntryDate = model.JalaliNewspaperEntryDate.ToDateTime();
                model.CompletionDate = model.JalaliCompletionDate.ToDateTime();
                model.LastElectionMeetingDate = model.JalaliLastElectionMeetingDate.ToDateTime();

                if (model.AppointmentDate > DateTime.Now)
                {
                    ModelState.AddModelError("", Resources.Messages.InvalidDate);
                    return View(model);
                }

                if (model.IsActive)
                {
                    var numberOfCompanyBoardMembers = company.GetAsQueryable(x => x.Id == model.CompanyId).Select(c => c.NumberOfCompanyBoardMembers).FirstOrDefault();

                    if (numberOfCompanyBoardMembers.HasValue)
                    {
                        var currentBoardMembersCount = _companyBoardMemberRepository.GetAsQueryable(x => x.CompanyId == model.CompanyId && x.IsActive).Count();
                        if (currentBoardMembersCount >= numberOfCompanyBoardMembers.Value)
                        {
                            ModelState.AddModelError("", string.Format(Resources.Messages.MaxNumberOfBoardMembersReached, numberOfCompanyBoardMembers.Value));
                            return View(model);
                        }
                    }
                }
          
                var item = _mapper.Map<CompanyBoardMemberViewModel, CompanyBoardMember>(model);
                await _companyBoardMemberRepository.InsertAsync(item);

                ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;
                if (Request.Form.Keys.Contains("SaveAndReturn"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Edit", new { id = item.Id });
            }
            return View(model);
        }
  

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            
            var item = await _companyBoardMemberRepository.GetByIDAsync(id);
            HttpContext.Session.SetString("companyId", item.CompanyId.ToString());
            if (!HasAccess(item.CompanyId))
                return View("Error");

            var model = _mapper.Map<CompanyBoardMember, CompanyBoardMemberViewModel>(item);

            model.JalaliAppointmentDate= model.AppointmentDate.ToShamsi();
            model.JalaliCompletionDate = model.CompletionDate.ToShamsi();
            model.JalaliNewspaperEntryDate = model.NewspaperEntryDate.ToShamsi();
            model.JalaliCompletionDate = model.CompletionDate.ToShamsi();
            model.JalaliLastElectionMeetingDate = model.LastElectionMeetingDate.ToShamsi();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyBoardMemberViewModel model)
        {
            model.SystemUserId = UserId;

            if (!HasAccess(model.CompanyId))
                return View("Error");


            //if (ModelState.IsValid)
            {

                model.AppointmentDate = model.JalaliAppointmentDate.ToDateTime();
                model.CompletionDate = model.JalaliCompletionDate.ToDateTime();
                model.NewspaperEntryDate = model.JalaliNewspaperEntryDate.ToDateTime();
                model.CompletionDate = model.JalaliCompletionDate.ToDateTime();
                model.LastElectionMeetingDate = model.JalaliLastElectionMeetingDate.ToDateTime();

                if (model.AppointmentDate > DateTime.Now)
                {
                    ModelState.AddModelError("", Resources.Messages.InvalidDate);
                    return View(model);
                }

  
                var item = _mapper.Map<CompanyBoardMemberViewModel, CompanyBoardMember>(model);
                var result = await _companyBoardMemberRepository.UpdateAsync(item);

                ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;

                if (Request.Form.Keys.Contains("SaveAndReturn"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Edit", new { id = item.Id });
            }
            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var item = await _companyBoardMemberRepository.GetByIDAsync(id);

            if (!HasAccess(item.CompanyId))
                return View("Error");


            var result = await _companyBoardMemberRepository.DeleteAsync(id);
            if (result < 1)
            {
                if (TempData["Error"] != null)
                    TempData.Remove("Error");
                TempData.Add("Error", Resources.Messages.ProblemDeletingItem);
            }
            return RedirectToAction("Index");
        }
        [HttpGet(Name = "CompanyHistory")]
        public async Task<IActionResult> CompanyHistory(int id)
        {
            var item = await _companyBoardMemberRepository.GetByIDAsync(id);
            HttpContext.Session.SetString("companyId", item.CompanyId.ToString());
            var companyId = HttpContext.Session.GetString("companyId");
            IEnumerable<IGrouping<DateTime?, CompanyBoardMember>> items = _companyBoardMemberRepository.GetAsQueryable(x => x.CompanyId == Convert.ToInt32(companyId), includeProperties: "BoardofDirectorsLegalMember,AgentCompany,LegalMemberType,OrganizationalPosition,Company,Agent").ToList().GroupBy(x => x.AppointmentDate);

            //var model = _mapper.Map<IEnumerable<CompanyBoardMember>, IEnumerable<CompanyBoardMemberViewModel>>(items);
            return View(items);
        }

        public ActionResult Company_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(company.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult Company_Read_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_Company())
                    {
                        if (values.Contains(woak.Id))
                        {
                            indices.Add(index);
                        }

                        index += 1;
                    }
                }

                return Json(indices);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public IEnumerable<Company> TagHelper_Company()
        {
            try
            {
                return company.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public ActionResult LegalMemberType_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(_legalMemberTypeRepository.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult LegalMemberType_Read_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_LegalMemberType())
                    {
                        if (values.Contains(woak.Id))
                        {
                            indices.Add(index);
                        }

                        index += 1;
                    }
                }

                return Json(indices);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public IEnumerable<LegalMemberType> TagHelper_LegalMemberType()
        {
            try
            {
                return _legalMemberTypeRepository.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public ActionResult Agent_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(_agentRepository.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult Agent_Read_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_Agent())
                    {
                        if (values.Contains(woak.Id))
                        {
                            indices.Add(index);
                        }

                        index += 1;
                    }
                }

                return Json(indices);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public IEnumerable<Agent> TagHelper_Agent()
        {
            try
            {
                return _agentRepository.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public ActionResult BoardofDirectorsLegalMember_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(_boardofDirectorsLegalMemberRepository.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult BoardofDirectorsLegalMember_Read_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_Agent())
                    {
                        if (values.Contains(woak.Id))
                        {
                            indices.Add(index);
                        }

                        index += 1;
                    }
                }

                return Json(indices);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public IEnumerable<BoardofDirectorsLegalMember> TagHelper_BoardofDirectorsLegalMember()
        {
            try
            {
                return _boardofDirectorsLegalMemberRepository.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }
        public ActionResult OrganizationalPosition_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(_organizationalPositionRepository.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult OrganizationalPosition_Read_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_OrganizationalPosition())
                    {
                        if (values.Contains(woak.Id))
                        {
                            indices.Add(index);
                        }

                        index += 1;
                    }
                }

                return Json(indices);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public IEnumerable<OrganizationalPosition> TagHelper_OrganizationalPosition()
        {
            try
            {
                return _organizationalPositionRepository.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }
        public ActionResult AgentCompany_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(company.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult AgentCompany_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_AgentCompany())
                    {
                        if (values.Contains(woak.Id))
                        {
                            indices.Add(index);
                        }

                        index += 1;
                    }
                }

                return Json(indices);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public IEnumerable<Company> TagHelper_AgentCompany()
        {
            try
            {
                return company.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }
    }
}
