using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using AutoMapper;
using System.Net.Http;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Models;
using KavoshFrameWorkCommon.Extensions;
using KavoshFrameWorkCore;
using KavoshFrameWorkCommon.Helpers;
using System.Security.Claims;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Serilog;

namespace KavoshFrameWorkWebApplication.Controllers
{

    public class CompanyController : BaseController
    {
        IMapper _mapper;
        IGenericRepository<Company> _companyRepository;
        IGenericRepository<CompanyMedia> _companyMediaRepository;
        IGenericRepository<CompanyBoardMember> _companyBoardMemberRepository;
        IGenericRepository<CompanyType> _companyTypeRepository;
        IGenericRepository<CompanyActivityType> _companyActivityType;
        IGenericRepository<CompanyPartnershipType> _companyPartnershipType;
        private readonly IEntityService entityService;

        public CompanyController(IGenericRepository<Company> companyRepository, IMapper mapper,
            IGenericRepository<CompanyMedia> companyMediaRepository,
            IGenericRepository<CompanyBoardMember> companyBoardMemberRepository,
            IGenericRepository<CompanyUser> companyUserRepository,
            IGenericRepository<CompanyType> companyTypeRepository,
            IGenericRepository<CompanyActivityType> companyActivityType,
            IGenericRepository<CompanyPartnershipType> companyPartnershipType,
            UserManager<ApplicationUser> userManager, IEntityService _entityService)
        {
            _companyRepository = companyRepository;
            _companyMediaRepository = companyMediaRepository;
            _mapper = mapper;
            this._companyUserRepository = companyUserRepository;
            this._userManager = userManager;
            _companyBoardMemberRepository = companyBoardMemberRepository;
            _companyTypeRepository = companyTypeRepository;
            _companyActivityType = companyActivityType;
            _companyPartnershipType= companyPartnershipType;
            entityService = _entityService;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.SystemAction = await entityService.GetUserAllowedActionFor(User.Identity.Name, typeof(Company));
            if ((((SystemAction)ViewBag.SystemAction) & SystemAction.Update) == 0)
                return Forbid();
            List<Company> items;
            if (User.IsInRole("Admin"))
                items = _companyRepository.GetAsQueryable().ToList();
            else
                items = _companyRepository.GetAsQueryable(x => CompanyIds.Contains(x.Id)).ToList();
            var model = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(items);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new CompanyViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CompanyViewModel model)
        {
            model.SystemUserId = UserId;

            if (ModelState.IsValid)
            {
                model.RegistrationDate = model.JalaliRegistrationDate.ToDateTime();
                model.EstablishmentDate = model.JalaliEstablishmentDate.ToDateTime();

                if (model.RegistrationDate.HasValue && model.RegistrationDate > DateTime.Now)
                {
                    ModelState.AddModelError("", Resources.Messages.InvalidDate);
                    return View(model);
                }

                if (model.EstablishmentDate.HasValue && model.EstablishmentDate > DateTime.Now)
                {
                    ModelState.AddModelError("", Resources.Messages.InvalidDate);
                    return View(model);
                }

                var item = _mapper.Map<CompanyViewModel, Company>(model);
                await _companyRepository.InsertAsync(item);

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
            if (!HasAccess(id))
                return View("Error");

            var item = await _companyRepository.GetByIDAsync(id);
            var model = _mapper.Map<Company, CompanyViewModel>(item);
            model.JalaliRegistrationDate = model.RegistrationDate.ToShamsi();
            model.JalaliEstablishmentDate = model.EstablishmentDate.ToShamsi();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyViewModel model)
        {
            model.SystemUserId = UserId;

            if (!HasAccess(model.Id))
                return View("Error");

            if (ModelState.IsValid)
            {
                model.RegistrationDate = model.JalaliRegistrationDate.ToDateTime();
                if (model.RegistrationDate.HasValue && model.RegistrationDate > DateTime.Now)
                {
                    ModelState.AddModelError("", Resources.Messages.InvalidDate);
                    return View(model);
                }

                model.EstablishmentDate = model.JalaliEstablishmentDate.ToDateTime();
                if (model.EstablishmentDate.HasValue && model.EstablishmentDate > DateTime.Now)
                {
                    ModelState.AddModelError("", Resources.Messages.InvalidDate);
                    return View(model);
                }

                var item = _mapper.Map<CompanyViewModel, Company>(model);
                var result = await _companyRepository.UpdateAsync(item);

                ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;

                if (Request.Form.Keys.Contains("SaveAndReturn"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Edit", new { id = item.Id });
            }
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!HasAccess(id))
                return View("Error");

            var result = await _companyRepository.RemoveAsync(id);

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
           // var item = await _companyBoardMemberRepository.GetByIDAsync(id);
            HttpContext.Session.SetString("companyId", id.ToString());
            int companyId = Convert.ToInt32(HttpContext.Session.GetString("companyId"));

            var items = await _companyBoardMemberRepository.GetAsync(x => x.CompanyId == companyId, includeProperties: "BoardofDirectorsLegalMember,AgentCompany,LegalMemberType,OrganizationalPosition,Company,Agent");
            IEnumerable<IGrouping<DateTime?, CompanyBoardMember>> result = items.GroupBy(x => x.AppointmentDate);
            //var model = _mapper.Map<IEnumerable<CompanyBoardMember>, IEnumerable<CompanyBoardMemberViewModel>>(items);
            return View("~/Views/CompanyBoardMember/CompanyHistory.cshtml", items);
        }

        ////////////////////////////////////////////////////////////////
        public ActionResult CompanyType_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(_companyTypeRepository.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult CompanyType_Read_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_CompanyType())
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
        public IEnumerable<CompanyType> TagHelper_CompanyType()
        {
            try
            {
                return _companyTypeRepository.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public ActionResult CompanyActivityType_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(_companyActivityType.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult CompanyActivityType_Read_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_CompanyActivityType())
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
        public IEnumerable<CompanyActivityType> TagHelper_CompanyActivityType()
        {
            try
            {
                return _companyActivityType.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }
        public ActionResult CompanyPartnershipType_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(_companyPartnershipType.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult CompanyPartnershipType_Read_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_CompanyPartnershipType())
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
        public IEnumerable<CompanyPartnershipType> TagHelper_CompanyPartnershipType()
        {
            try
            {
                return _companyPartnershipType.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }
    }
}
