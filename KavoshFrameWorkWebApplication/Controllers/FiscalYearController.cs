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
using Serilog;
using Kendo.Mvc.Extensions;

namespace KavoshFrameWorkWebApplication.Controllers
{

    public class FiscalYearController : BaseController
    {
        IMapper _mapper;
        IGenericRepository<FiscalYear> _fiscalYearRepository;
        IGenericRepository<Company> _companyRepository;
        public FiscalYearController(IGenericRepository<FiscalYear> fiscalYearRepository,
         UserManager<ApplicationUser> userManager, IMapper mapper, IGenericRepository<CompanyUser> companyUserRepository,
         IGenericRepository<Company> companyRepository)
        {
            _fiscalYearRepository = fiscalYearRepository;
            _mapper = mapper;
            this._userManager = userManager;
            this._companyUserRepository = companyUserRepository;
            this._companyRepository = companyRepository;

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var items = _fiscalYearRepository.GetAsQueryable(includeProperties: "Company").ToList();

            var model = _mapper.Map<IEnumerable<FiscalYear>, IEnumerable<FiscalYearViewModel>>(items);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new FiscalYearViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(FiscalYearViewModel model)
        {
            model.SystemUserId = UserId;

            if (ModelState.IsValid)
            {
                model.StartDate = model.JalaliStartDate.ToDateTime();
                model.EndDate = model.JalaliEndDate.ToDateTime();

                var item = _mapper.Map<FiscalYearViewModel, FiscalYear>(model);
                await _fiscalYearRepository.InsertAsync(item);

                ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;
                if (Request.Form.Keys.Contains("SaveAndReturn"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Edit", new { id = item.Id });
            }
            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _fiscalYearRepository.GetByIDAsync(id);
            var model = _mapper.Map<FiscalYear, FiscalYearViewModel>(item);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(FiscalYearViewModel model)
        {
            model.SystemUserId = UserId;

            if (ModelState.IsValid)
            {

                model.StartDate = model.JalaliStartDate.ToDateTime();
                model.EndDate = model.JalaliEndDate.ToDateTime();

                var item = _mapper.Map<FiscalYearViewModel, FiscalYear>(model);
                var result = await _fiscalYearRepository.UpdateAsync(item);

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
            var result = await _fiscalYearRepository.RemoveAsync(id);
            if (result < 1)
            {
                if (TempData["Error"] != null)
                    TempData.Remove("Error");
                TempData.Add("Error", Resources.Messages.ProblemDeletingItem);
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult GetByCompany(int id)
        {
            var result = _fiscalYearRepository.GetAsQueryable(x => x.CompanyId == id).Select(x => new KeyTitleViewModel
            {
                Id = x.Id,
                Title = x.Title
            }).ToList();

            return View("_FiscalYears",result);
        }

        public ActionResult FiscalYear_Read([DataSourceRequest] DataSourceRequest request)
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
        public ActionResult FiscalYear_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_Application())
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
        public IEnumerable<Company> TagHelper_Application()
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

    }
}
