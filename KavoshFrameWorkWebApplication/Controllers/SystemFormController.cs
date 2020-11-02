using AutoMapper;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkWebApplication.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Controllers
{
    public class SystemFormController : BaseController
    {
        IMapper _mapper;
        IGenericRepository<SystemForm> _systemFormRepository;
        public SystemFormController(IGenericRepository<SystemForm> systemFormRepository, IMapper mapper,
        UserManager<ApplicationUser> userManager)
        {
            _systemFormRepository = systemFormRepository;
            _mapper = mapper;
            this._userManager = userManager;

        }
        public IActionResult Index()
        {
            try
            {
                var items = _systemFormRepository.GetAsQueryable();
                var model = _mapper.Map<IEnumerable<SystemForm>, IEnumerable<SystemFormViewModel>>(items);
                return View(model);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View(new SystemFormViewModel());

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SystemFormViewModel model)
        {
            try
            {
                model.SystemUserId = UserId;
                if (ModelState.IsValid)
                {

                    var item = _mapper.Map<SystemFormViewModel, SystemForm>(model);
                    await _systemFormRepository.InsertAsync(item);

                    ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;
                    if (Request.Form.Keys.Contains("SaveAndReturn"))
                        return RedirectToAction("Index");
                    else
                        return RedirectToAction("Edit", new { id = item.Id });
                }
                return View(model);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                SystemForm item = await _systemFormRepository.GetByIDAsync(id);
                SystemFormViewModel model = _mapper.Map<SystemForm, SystemFormViewModel>(item);
                return View(model);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SystemFormViewModel model)
        {
            try
            {
                model.SystemUserId = UserId;

                if (ModelState.IsValid)
                {

                    var item = _mapper.Map<SystemFormViewModel, SystemForm>(model);
                    // _systemFormRepository.DetachRepository(item, model.Id);
                    var result = await _systemFormRepository.UpdateAsync(item);

                    ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;

                    if (Request.Form.Keys.Contains("SaveAndReturn"))
                        return RedirectToAction("Index");
                    else
                        return RedirectToAction("Edit", new { id = item.Id });
                }
                return View(model);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _systemFormRepository.RemoveAsync(id);

                if (result < 1)
                {
                    if (TempData["Error"] != null)
                        TempData.Remove("Error");
                    TempData.Add("Error", Resources.Messages.ProblemDeletingItem);
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult ApplicationRole_EntityName([DataSourceRequest] Kendo.Mvc.UI.DataSourceRequest request)
        {
            try
            {
                return Json(_systemFormRepository.GetAll().ToList().ToDataSourceResult(request));

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult ApplicationRole_ValueMapper(int?[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_EntityName())
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
        public IEnumerable<SystemForm> TagHelper_EntityName()
        {
            try
            {
                return _systemFormRepository.GetAll().ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

    }
}