using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KavoshFrameWorkCommon.Helpers;
using KavoshFrameWorkCore;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Controllers
{
    [Authorize]
    public class EnactmentController : BaseController
    {
        IMapper _mapper;
        IEnactmentRepository _EnactmentRepository;

        public EnactmentController(IEnactmentRepository EnactmentRepository, IMapper mapper, UserManager<ApplicationUser> userManager, IGenericRepository<CompanyUser> companyUserRepository)
        {
            _mapper = mapper;
            this._userManager = userManager;
            this._companyUserRepository = companyUserRepository;
            this._EnactmentRepository = EnactmentRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Enactment> items;
            if (User.IsInRole("Admin"))
                items = await _EnactmentRepository.Enactments();
            else
                items = await _EnactmentRepository.Enactments(x => HasAccess(x.CompanyId));

            var model = _mapper.Map<IEnumerable<Enactment>, IEnumerable<EnactmentViewModel>>(items);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["MediaSettings"] = true;
            var item = new EnactmentViewModel();
            item.EnactmentDetails = new List<EnactmentDetail>()
            {
                new EnactmentDetail {EnactmentTitle = " " }
            };
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnactmentViewModel model)
        {
            model.SystemUserId = UserId;

            if (ModelState.IsValid)
            {
                model.Date = model.JalaliDate.ToDateTime().Value;
                if (model.Date > DateTime.Now)
                {
                    ModelState.AddModelError("", Resources.Messages.InvalidDate);
                    return View(model);
                }
                var item = _mapper.Map<EnactmentViewModel, Enactment>(model);

                //int cnt = item.EnactmentDetails.Count;
                //for (int i = 0; i <= cnt - 1; i++)
                //{
                //    if (item.EnactmentDetails[i].EnactmentTitle == null)
                //        item.EnactmentDetails.Remove(item.EnactmentDetails[i]);
                //    else if (item.EnactmentDetails[i].EnactmentTitle.Equals(" ") || string.IsNullOrEmpty(item.EnactmentDetails[i].EnactmentTitle))
                //        item.EnactmentDetails.Remove(item.EnactmentDetails[i]);
                //}

                await _EnactmentRepository.InsertAsync(item);

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
            HttpContext.Session.Clear();
            var item = await _EnactmentRepository.GetByIDAsync(id);
            var model = _mapper.Map<Enactment, EnactmentViewModel>(item);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EnactmentViewModel model)
        {
            model.SystemUserId = UserId;

            if (ModelState.IsValid)
            {
                model.Date = model.JalaliDate.ToDateTime().Value;
                if (model.Date > DateTime.Now)
                {
                    ModelState.AddModelError("", Resources.Messages.InvalidDate);
                    return View(model);
                }

                var item = _mapper.Map<EnactmentViewModel, Enactment>(model);
                if (model.EnactmentTitle != " " && model.EnactmentTitle != null && !string.IsNullOrEmpty(model.EnactmentTitle))
                {
                    if (item.EnactmentDetails != null)
                    {
                        item.EnactmentDetails.Add(new EnactmentDetail { EnactmentTitle = model.EnactmentTitle });
                    }
                    else
                    {
                        item.EnactmentDetails = new List<EnactmentDetail>()
                        {
                            new EnactmentDetail{EnactmentTitle = model.EnactmentTitle}
                        };
                    }
                }
                var result = await _EnactmentRepository.UpdateAsync(item);

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
            var result = await _EnactmentRepository.DeleteAsync(id);
            if (result < 1)
            {
                if (TempData["Error"] != null)
                    TempData.Remove("Error");
                TempData.Add("Error", Resources.Messages.ProblemDeletingItem);
            }
            return RedirectToAction("Index");
        }
        [HttpPost(Name = "ChooseDetail")]
        public async Task<IActionResult> ChooseDetail(Tag tag)
        {
            var res = await _EnactmentRepository.GetByIDAsync(tag.EnactmentId);
            var model = _mapper.Map<Enactment, EnactmentViewModel>(res);

            HttpContext.Session.SetInt32("DetailId", tag.Id);

            model.DetailId = tag.Id;
            return RedirectToAction("Edit", new { id = tag.EnactmentId });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDetail(Tag tag)
        {
            await _EnactmentRepository.DeleteDetailAsync(tag.Id);
            ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;
            return Ok(new { response = "مصوبه جاری حذف شد" });
        }

    }
    public class Tag { public int Id { get; set; } public int EnactmentId { get; set; } }
}