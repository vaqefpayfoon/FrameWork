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
using FormStatus = KavoshFrameWorkWebApplication.Models.FormStatus;
using Serilog;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace KavoshFrameWorkWebApplication.Controllers
{
    public class CompanyDocumentController : BaseController
    {
        IMapper _mapper;
        IGenericRepository<CompanyDocument> _companyDocumentRepository;
        IGenericRepository<Company> _companyRepository;
        IGenericRepository<FiscalYear> _fiscalYearRepository;
        IGenericRepository<MainGroup> _mainGroupRepository;
        IGenericRepository<SubGroup> _subGroupRepository;
        private readonly IEntityService entityService;

        public CompanyDocumentController(IGenericRepository<CompanyDocument> companyDocumentRepository, IMapper mapper,
        UserManager<ApplicationUser> userManager,
                     IGenericRepository<CompanyUser> companyUserRepository,
                     IGenericRepository<Company> companyRepository,
                     IGenericRepository<FiscalYear> fiscalYearRepository,
                     IGenericRepository<MainGroup> mainGroupRepository,
                     IGenericRepository<SubGroup> subGroupRepository, IEntityService _entityService)
        {
            _companyDocumentRepository = companyDocumentRepository;
            _mapper = mapper;
            this._userManager = userManager;
            this._companyUserRepository = companyUserRepository;
            _companyRepository = companyRepository;
            _fiscalYearRepository = fiscalYearRepository;
            _mainGroupRepository = mainGroupRepository;
            _subGroupRepository = subGroupRepository;
            entityService = _entityService;
        }

        public async Task<IActionResult> Index()
        {

            ViewBag.SystemAction = await entityService.GetUserAllowedActionFor(User.Identity.Name, typeof(Company));
            if ((((SystemAction)ViewBag.SystemAction) & SystemAction.Update) == 0)
                return Forbid();

            List<CompanyDocument> items;

            if (User.IsInRole("Admin"))
                items = _companyDocumentRepository.GetAsQueryable(includeProperties: "MainGroup,SubGroup,Company,FiscalYear").ToList();
            else
                items = _companyDocumentRepository.GetAsQueryable(x => CompanyIds.Contains(x.Id), includeProperties: "MainGroup,SubGroup,Company,FiscalYear").ToList();

            var model = _mapper.Map<IEnumerable<CompanyDocument>, IEnumerable<CompanyDocumentViewModel>>(items);

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["MediaSettings"] = true;
            return View(new CompanyDocumentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyDocumentViewModel model)
        {
            model.SystemUserId = UserId;

            if (ModelState.IsValid)
            {
                ViewBag.Active = "Create";
                var item = _mapper.Map<CompanyDocumentViewModel, CompanyDocument>(model);
                await _companyDocumentRepository.InsertAsync(item);

                ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;
                if (Request.Form.Keys.Contains("SaveAndReturn"))
                    return RedirectToAction("Index");
                else
                {
                    return RedirectToAction("Edit", new { id = item.Id });
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _companyDocumentRepository.GetByIDAsync(id);
            var model = _mapper.Map<CompanyDocument, CompanyDocumentViewModel>(item);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyDocumentViewModel model)
        {
            model.SystemUserId = UserId;

            if (ModelState.IsValid)
            {
                var item = _mapper.Map<CompanyDocumentViewModel, CompanyDocument>(model);
                var result = await _companyDocumentRepository.UpdateAsync(item);

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
            var result = await _companyDocumentRepository.DeleteAsync(id);
            if (result < 1)
            {
                if (TempData["Error"] != null)
                    TempData.Remove("Error");
                TempData.Add("Error", Resources.Messages.ProblemDeletingItem);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Company_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(_companyRepository.GetAll().ToList().ToDataSourceResult(request));
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
                return _companyRepository.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public ActionResult FiscalYear_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(_fiscalYearRepository.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult FiscalYear_Read_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_FiscalYear())
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
        public IEnumerable<FiscalYear> TagHelper_FiscalYear()
        {
            try
            {
                return _fiscalYearRepository.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }


        public ActionResult MainGroup_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(_mainGroupRepository.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult MainGroup_Read_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_MainGroup())
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
        public IEnumerable<MainGroup> TagHelper_MainGroup()
        {
            try
            {
                return _mainGroupRepository.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public ActionResult SubGroup_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(_subGroupRepository.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult SubGroup_Read_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_SubGroup())
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
        public IEnumerable<SubGroup> TagHelper_SubGroup()
        {
            try
            {
                return _subGroupRepository.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }
    }
}
