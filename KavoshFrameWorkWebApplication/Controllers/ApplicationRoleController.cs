using AutoMapper;
using KavoshFrameWorkCore;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Controllers
{
    public class ApplicationRoleController : BaseController
    {        
        private readonly KavoshFrameWorkContext _context;
        readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationRoleController(KavoshFrameWorkContext context, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            try
            {
                var model = _roleManager.Roles.ToList();


                ApplicationRoleViewModel modelRole;

                List<ApplicationRoleViewModel> modelRoleList = new List<ApplicationRoleViewModel>();
                foreach (var item in model)
                {

                    modelRole = new ApplicationRoleViewModel();

                    modelRole.Title = item.Name;
                    modelRole.Id = item.Id;

                    modelRoleList.Add(modelRole);
                }
                return View(modelRoleList);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
            
        }

        [HttpGet]
        //  //[Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            try
            {
                var model = new ApplicationRoleViewModel();

                return View(model);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        [HttpPost]
       // // [ValidateAntiForgeryToken]
        //  //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ApplicationRoleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (await _roleManager.FindByNameAsync(model.Title.ToString()) == null)
                    {
                        await _roleManager.CreateAsync(new ApplicationRole
                        {
                            Name = model.Title.ToString(),
                            NormalizedName = model.Title.ToString().ToUpper()
                        });
                    }

                    if (Request.Form.Keys.Contains("SaveAndReturn"))
                        return RedirectToAction("Index");
                    else
                        return RedirectToAction("Edit", new { id = model.Id });
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
        public IActionResult Edit(string id)
        {
            try
            {
                var model = _roleManager.Roles.Where(x => x.Id == id);

                ApplicationRoleViewModel modelRole = new ApplicationRoleViewModel();

                modelRole.Title = model.Select(x => x.Name).FirstOrDefault();
                modelRole.Id = model.Select(x => x.Id).FirstOrDefault();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }



        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                IdentityResult result = new IdentityResult();
                var model = _roleManager.Roles.Where(x => x.Id == id);

                ApplicationRole modelRole = new ApplicationRole();
                modelRole.Id = model.Select(x => x.Id).FirstOrDefault();
                modelRole.Name = model.Select(x => x.Name).FirstOrDefault();
                modelRole.ConcurrencyStamp = model.Select(x => x.ConcurrencyStamp).FirstOrDefault();
                try
                {
                    result = await _roleManager.DeleteAsync(modelRole);
                    if (result.Succeeded)
                    {
                        if (TempData["Sucsses"] != null)
                            TempData.Remove("Sucsses");
                        TempData.Add("Sucsses", Resources.Messages.ChangesSavedSuccessfully);
                    }
                }
                catch (Exception e)
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