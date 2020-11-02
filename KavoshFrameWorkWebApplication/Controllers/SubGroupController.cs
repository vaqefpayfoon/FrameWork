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

namespace KavoshFrameWorkWebApplication.Controllers
{

    public class SubGroupController : BaseController
    {
        IMapper _mapper;
        IGenericRepository<SubGroup> _subGroupRepository;

        public SubGroupController(IGenericRepository<SubGroup> subGroupRepository,
         UserManager<ApplicationUser> userManager,
         IMapper mapper,
                 IGenericRepository<CompanyUser> companyUserRepository)
        {
            _subGroupRepository = subGroupRepository;
            _mapper = mapper;
            this._userManager = userManager;
            this._companyUserRepository = companyUserRepository;

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var items = _subGroupRepository.GetAsQueryable(includeProperties: "MainGroup").ToList();

            var model = _mapper.Map<IEnumerable<SubGroup>, IEnumerable<SubGroupViewModel>>(items);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new SubGroupViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(SubGroupViewModel model)
        {
            model.SystemUserId = UserId;

            if (ModelState.IsValid)
            {
              
                var item = _mapper.Map<SubGroupViewModel, SubGroup>(model);
                await _subGroupRepository.InsertAsync(item);

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
            var item = await _subGroupRepository.GetByIDAsync(id);
            var model = _mapper.Map<SubGroup, SubGroupViewModel>(item);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(SubGroupViewModel model)
        {
            model.SystemUserId = UserId;

            if (ModelState.IsValid)
            {

                var item = _mapper.Map<SubGroupViewModel, SubGroup>(model);
                var result = await _subGroupRepository.UpdateAsync(item);

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
            var result = await _subGroupRepository.RemoveAsync(id);
            if (result < 1)
            {
                if (TempData["Error"] != null)
                    TempData.Remove("Error");
                TempData.Add("Error", Resources.Messages.ProblemDeletingItem);
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult GetByMainGroup(int id)
        {
            var result = _subGroupRepository.GetAsQueryable(x => x.MainGroupId == id).Select(x => new KeyTitleViewModel
            {
                Id = x.Id,
                Title = x.Title
            }).ToList();

            return View("_SubGroups",result);
        }
    }
}
