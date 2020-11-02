using AutoMapper;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Controllers
{

    ////[Authorize(Roles = "Admin")]
    public class BaseGenericController<TEntity, TViewModel> : Controller where TEntity : BaseEntity where TViewModel : BaseBaseViewModel
    {
        IMapper _mapper;
        private readonly IEntityService entityService;
        IGenericRepository<TEntity> _repository;

        public BaseGenericController(IGenericRepository<TEntity> repository, IMapper mapper, IEntityService entityService)
        {
            _repository = repository;
            _mapper = mapper;
            this.entityService = entityService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.SystemAction = await entityService.GetUserAllowedActionFor(User.Identity.Name, typeof(TEntity));

                if ((((SystemAction)ViewBag.SystemAction) & SystemAction.Read) == 0)
                    return Forbid();

                var items = await _repository.GetAllAsync();
                var model = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TViewModel>>(items);

                return View("~/Views/Base/Index.cshtml", model);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.SystemAction = await entityService.GetUserAllowedActionFor(User.Identity.Name, typeof(TEntity));

                if ((((SystemAction)ViewBag.SystemAction) & SystemAction.Create) == 0)
                    return Forbid();

                return View("~/Views/Base/Create.cshtml");
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }

        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TViewModel model)
        {
            try
            {
                ViewBag.SystemAction = await entityService.GetUserAllowedActionFor(User.Identity.Name, typeof(TEntity));

                if ((((SystemAction)ViewBag.SystemAction) & SystemAction.Create) == 0)
                    return Forbid();


                model.SystemUserId = User.FindFirst(x => x.Type == "sub" || x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (ModelState.IsValid)
                {
                    var item = _mapper.Map<TViewModel, TEntity>(model);

                    if (_repository.GetAsQueryable(x => x.Title == item.Title).Any())
                    {
                        ModelState.AddModelError("", Resources.Messages.RedundantRowAdded);
                        return View("~/Views/Base/Create.cshtml", model);
                    }

                    await _repository.InsertAsync(item);

                    if (Request.Form.Keys.Contains("SaveAndReturn"))
                        return RedirectToAction("Index");
                    else
                        return RedirectToAction("Edit", new { id = item.Id });
                }
                return View("~/Views/Base/Create.cshtml", model);
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
                ViewBag.SystemAction = await entityService.GetUserAllowedActionFor(User.Identity.Name, typeof(TEntity));


                var item = await _repository.GetByIDAsync(id);
                var model = _mapper.Map<TEntity, TViewModel>(item);

                return View("~/Views/Base/Edit.cshtml", model);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TViewModel model)
        {
            try
            {
                ViewBag.SystemAction = await entityService.GetUserAllowedActionFor(User.Identity.Name, typeof(TEntity));

                if ((((SystemAction)ViewBag.SystemAction) & SystemAction.Update) == 0)
                    return Forbid();

                model.SystemUserId = User.FindFirst(x => x.Type == "sub" || x.Type == ClaimTypes.NameIdentifier)?.Value;

                if (ModelState.IsValid)
                {
                    var item = _mapper.Map<TViewModel, TEntity>(model);

                    if (_repository.GetAsQueryable(x => x.Title == item.Title && x.Id != item.Id).Any())
                    {
                        ModelState.AddModelError("", Resources.Messages.RedundantRowAdded);
                        return View("~/Views/Base/Create.cshtml", model);
                    }


                    var result = await _repository.UpdateAsync(item);

                    if (Request.Form.Keys.Contains("SaveAndReturn"))
                        return RedirectToAction("Index");
                    else
                        return RedirectToAction("Edit", new { id = item.Id });
                }
                return View("~/Views/Base/Edit.cshtml", model);
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
                ViewBag.SystemAction = await entityService.GetUserAllowedActionFor(User.Identity.Name, typeof(TEntity));

                if ((((SystemAction)ViewBag.SystemAction) & SystemAction.Delete) == 0)
                    return Forbid();


                var result = await _repository.RemoveAsync(id);
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

    }

}
