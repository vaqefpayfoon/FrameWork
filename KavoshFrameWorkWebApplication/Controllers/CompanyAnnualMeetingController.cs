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
    public class CompanyAnnualMeetingController : BaseController
    {
        IMapper _mapper;
        IGenericRepository<CompanyAnnualMeeting> _companyAnnualMeetingRepository;
        IGenericRepository<CompanyShareholder> _companyShareholderRepository;
        IGenericRepository<Company> _companyRepository;
        IGenericRepository<FiscalYear> _fiscalYearRepository;
        IGenericRepository<MainGroup> _mainGroupRepository;
        IGenericRepository<SubGroup> _subGroupRepository;
        IGenericRepository<Auditor> _auditorRepository;
        IGenericRepository<Newspaper> _newspaperRepository;
        private readonly IEntityService entityService;

        public CompanyAnnualMeetingController(IGenericRepository<CompanyAnnualMeeting> companyAnnualMeetingRepository,
         UserManager<ApplicationUser> userManager, IMapper mapper, IGenericRepository<CompanyShareholder> companyShareholderRepository, 
                     IGenericRepository<CompanyUser> companyUserRepository, IGenericRepository<Company> companyRepository,
                     IGenericRepository<FiscalYear> fiscalYearRepository, IGenericRepository<MainGroup> mainGroupRepository, IGenericRepository<SubGroup> subGroupRepository, IGenericRepository<Auditor> auditorRepository,
                     IGenericRepository<Newspaper> newspaperRepository, IEntityService _entityService)
        {
            _companyAnnualMeetingRepository = companyAnnualMeetingRepository;
            _mapper = mapper;
            this._userManager = userManager;
            this._companyUserRepository = companyUserRepository;
            this._companyShareholderRepository = companyShareholderRepository;
            _companyRepository = companyRepository;
            _fiscalYearRepository = fiscalYearRepository;
            _mainGroupRepository = mainGroupRepository;
            _subGroupRepository = subGroupRepository;
            _auditorRepository = auditorRepository;
            _newspaperRepository = newspaperRepository;
            entityService = _entityService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.SystemAction = await entityService.GetUserAllowedActionFor(User.Identity.Name, typeof(Company));
            if ((((SystemAction)ViewBag.SystemAction) & SystemAction.Update) == 0)
                return Forbid();

            List<CompanyAnnualMeeting> items;
            
            if (User.IsInRole("Admin"))
                items = _companyAnnualMeetingRepository.GetAsQueryable(includeProperties: "MainGroup,SubGroup,FiscalYear,Company").ToList();
            else
                items = items = _companyAnnualMeetingRepository.GetAsQueryable(x => CompanyIds.Contains(x.Id), includeProperties: "MainGroup,SubGroup,FiscalYear,Company").ToList(); 
            var model = _mapper.Map<IEnumerable<CompanyAnnualMeeting>, IEnumerable<CompanyAnnualMeetingViewModel>>(items);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CompanyAnnualMeetingViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyAnnualMeetingViewModel model)
        {
            model.SystemUserId = UserId;

            //if (ModelState.IsValid)
            {
                model.Date = model.JalaliDate.ToDateTime().Value;
                if (model.Date > DateTime.Now)
                {
                    ModelState.AddModelError("", Resources.Messages.InvalidDate);
                    return View(model);
                }
                var item = _mapper.Map<CompanyAnnualMeetingViewModel, CompanyAnnualMeeting>(model);
                item.NewspaperId2 = 1;
                item.SecondaryAuditorId = 1;
                await _companyAnnualMeetingRepository.InsertAsync(item);

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
            var item = await _companyAnnualMeetingRepository.GetByIDAsync(id);
            var model = _mapper.Map<CompanyAnnualMeeting, CompanyAnnualMeetingViewModel>(item);

            model.JalaliDate = model.Date.ToShamsi();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyAnnualMeetingViewModel model)
        {
            model.SystemUserId = UserId;

            //if (ModelState.IsValid)
            {
                
                model.Date = model.JalaliDate.ToDateTime().Value;
                if (model.Date > DateTime.Now)
                {
                    ModelState.AddModelError("", Resources.Messages.InvalidDate);
                    return View(model);
                }

                var item = _mapper.Map<CompanyAnnualMeetingViewModel, CompanyAnnualMeeting>(model);
                item.NewspaperId2 = 1;
                item.SecondaryAuditorId = 1;
                var result = await _companyAnnualMeetingRepository.UpdateAsync(item);

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
            var result = await _companyAnnualMeetingRepository.DeleteAsync(id);
            if (result < 1)
            {
                if (TempData["Error"] != null)
                    TempData.Remove("Error");
                TempData.Add("Error", Resources.Messages.ProblemDeletingItem);
            }
            return RedirectToAction("Index");
        }

        [HttpPost(Name = "ReloadShare")]
        public async Task<IActionResult> ReloadShare(Tag tag)
        {
            var res = await _companyShareholderRepository.GetAllAsync();
            var item = res.LastOrDefault(woak => woak.CompanyId == tag.Id);
            if (item == null)
                return Ok();
            return Ok(new { shares = item.Shares });
        }
        //////////////////////////////////////////////////////////////
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

        public ActionResult PrimaryAuditor_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(_auditorRepository.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult Auditor_Read_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_Auditor())
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
        public IEnumerable<Auditor> TagHelper_Auditor()
        {
            try
            {
                return _auditorRepository.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public ActionResult Newspaper_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(_newspaperRepository.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult Newspaper_Read_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_Newspaper())
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
        public IEnumerable<Newspaper> TagHelper_Newspaper()
        {
            try
            {
                return _newspaperRepository.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

    }
}
