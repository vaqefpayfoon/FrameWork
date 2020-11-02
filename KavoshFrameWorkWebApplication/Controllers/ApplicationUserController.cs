using AutoMapper;
using KavoshFrameWorkCommon.Helpers;
using KavoshFrameWorkCore;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkData.Repositories;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkWebApplication.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Telerik.DataSource;
using static KavoshFrameWorkWebApplication.Helpers.Utility;

namespace KavoshFrameWorkWebApplication.Controllers
{
    [AllowAnonymous]
    public class ApplicationUserController : BaseController
    {
        IMapper _mapper;
        private readonly KavoshFrameWorkContext _context;
        readonly SignInManager<ApplicationUser> _signInManager;
        readonly RoleManager<ApplicationRole> _roleManager;
        readonly IGenericRepository<Company> _companyViewData;
        readonly IApplicationUserRepository _user;
        public ApplicationUserController(RoleManager<ApplicationRole> roleManager
            ,UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            ,IMapper mapper, IGenericRepository<CompanyUser> companyUserRepository
            , KavoshFrameWorkContext context
            , IGenericRepository<Company> companyViewData, IApplicationUserRepository user)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _companyViewData = companyViewData;
            _companyUserRepository = companyUserRepository;
            this._context = context;
            _user = user;
        }
        public IActionResult Index()
        {
            try
            {
                var model = _user.GetAsQueryable().ToList().Select(x => new UserViewModel
                {
                    Mobile = x.Mobile,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserName = x.UserName,
                    PhoneNumber = x.PhoneNumber,
                    Id = x.Id,
                });

                return View(model);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            try
            {
                if (model.Password == null)
                {
                    ModelState.AddModelError("", "رمز عبور وارد نشده است!");
                    return View(model);
                };

                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.UserName,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        Mobile = model.Mobile,
                        AddedDate = DateTime.Now,
                        RoleId = model.RoleId,
                    };
                    model.CompanyIds.Add(model.CompanyId);
                    if (model.CompanyIds != null && model.CompanyIds.Count > 0)
                    {
                        user.Companies = _companyViewData.GetAsQueryable(x => model.CompanyIds.Contains(x.Id))
                       .Select(x => new CompanyUser { CompanyId = x.Id }).ToList();
                    }

                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var systemRoles = _roleManager.Roles.ToList().Where(x => x.Id == model.RoleId);

                        var roles = systemRoles.Select(x => x.Name).FirstOrDefault();
                        if (roles != null)
                        {
                            await _userManager.AddToRoleAsync(user, roles.ToString());

                        }

                        if (Request.Form.Keys.Contains("SaveAndReturn"))
                            return RedirectToAction("Index");
                        else
                            return RedirectToAction("Edit", new { userId = user.Id });
                    }
                    else
                    {
                        AddErrors(result);
                    }
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
        public async Task<IActionResult> Edit(string Id)
        {
            try
            {
                var item = await _userManager.FindByIdAsync(Id);
                if (item != null)
                {
                    var model = _mapper.Map<ApplicationUser, UserViewModel>(item);

                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var item = await _userManager.FindByIdAsync(model.Id);

                    if (item != null)
                    {
                        item.FirstName = model.FirstName;
                        item.LastName = model.LastName;
                        item.Email = model.Email;
                        item.NormalizedEmail = model.Email?.ToUpper();
                        item.PhoneNumber = model.PhoneNumber;
                        item.UserName = model.UserName;
                        item.NormalizedEmail = model.UserName.ToUpper();
                        item.Mobile = model.Mobile;
                        item.LastModifiedDate = DateTime.Now;
                        item.RoleId = model.RoleId;
                        model.CompanyIds.Add(model.CompanyId);
                        if (!string.IsNullOrEmpty(model.Password))
                        {
                            var token = await _userManager.GeneratePasswordResetTokenAsync(item);
                            await _userManager.ResetPasswordAsync(item, token, model.Password);
                        }

                        if (model.CompanyIds != null && model.CompanyIds.Count > 0)
                        {
                            await _companyUserRepository.InsertAllAsync(_companyViewData.GetAsQueryable(x => model.CompanyIds.Contains(x.Id))
                           .Select(x => new CompanyUser { CompanyId = x.Id, UserId = model.Id }).ToList());
                        }

                        await _userManager.UpdateAsync(item);
                        //update 
                        var user = await _userManager.FindByIdAsync(model.Id);
                        var roles = await _userManager.GetRolesAsync(user);
                        await _userManager.RemoveFromRolesAsync(user, roles);

                        await _userManager.RemoveClaimsAsync(user, (await _userManager.GetClaimsAsync(user)));


                        var systemRoles = _roleManager.Roles.ToList().Where(x => x.Id == model.RoleId);

                        var roles1 = systemRoles.Select(x => x.Name).FirstOrDefault();

                        if (roles1 != null)
                        {
                            await _userManager.AddToRoleAsync(user, roles1.ToString());

                        }

                        if (Request.Form.Keys.Contains("SaveAndReturn"))
                            return RedirectToAction("Index");
                        else
                            return RedirectToAction("Edit", new { userId = item.Id });
                    }
                }

                return View(model);
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
                var user = await _userManager.FindByIdAsync(id);
                var result = await _userManager.DeleteAsync(user);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        private void AddErrors(IdentityResult result)
        {
            try
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
            }
        }
        public IActionResult ChangePassword()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(User.Identity.Name);
                    var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
                    if (result.Succeeded)
                    {
                        SuccessMessage = "رمز عبور با موفقیت تغییر کرد.";
                        return RedirectToAction("ChangePassword");
                    }
                    ModelState.AddModelError(string.Empty, "رمز عبور جاری صحیح وارد نشده است!");
                }
                return View(model);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public List<ApplicationRole> GetRoleListItem()
        {
            try
            {
                List<SelectListItem> lst = _roleManager.Roles.Select(woak => new SelectListItem
                {
                    Text = woak.Name,
                    Value = woak.Id.ToString()
                }).ToList();
                return _roleManager.Roles.ToList();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }
        [HttpGet]
        public JsonResult GetRoleItems(string text)
        {
            try
            {
                IEnumerable<ApplicationRole> roles = _roleManager.Roles;

                if (!string.IsNullOrEmpty(text))
                {
                    roles = roles.Where(p => p.Name.Contains(text));
                }
                return Json(roles.ToList());
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }
        public ActionResult ApplicationRole_Read([DataSourceRequest] Kendo.Mvc.UI.DataSourceRequest request)
        {
            try
            {
                return Json(_roleManager.Roles.ToList().ToDataSourceResult(request));

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult ApplicationRole_ValueMapper(string[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_ApplicationRoles())
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
        public IEnumerable<ApplicationRole> TagHelper_ApplicationRoles()
        {
            try
            {
                return _roleManager.Roles.ToList();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public ActionResult CompanyViewData_Read([DataSourceRequest] Kendo.Mvc.UI.DataSourceRequest request)
        {
            try
            {
                return Json(_companyViewData.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        public ActionResult CompanyViewData_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_CompanyViewData())
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
        public IEnumerable<Company> TagHelper_CompanyViewData()
        {
            try
            {
                return _companyViewData.GetAll();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }
    }

}
