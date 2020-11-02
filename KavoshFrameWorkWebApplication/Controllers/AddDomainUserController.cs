using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using KavoshFrameWorkCore;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkWebApplication.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.DirectoryServices.Protocols;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using KavoshFrameWorkData.Repositories.Ldap;
using Serilog;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace KavoshFrameWorkWebApplication.Controllers
{
    public class AddDomainUserController : BaseController
    {
        private readonly KavoshFrameWorkContext _context;

        IMapper _mapper;
        IEncriptDescriptString _encriptdescriptStringRepository;
        IPingLdap _pingLdap;
        IFindAllADUsers _findAllADUsers;
        readonly SignInManager<ApplicationUser> _signInManager;
        readonly RoleManager<ApplicationRole> _roleManager;
        IGenericRepository<DomainSetting> _domainRepository;        
        public AddDomainUserController(IGenericRepository<DomainSetting> domainRepository
            ,KavoshFrameWorkContext context
            ,IEncriptDescriptString encriptdescriptStringRepository
            ,IFindAllADUsers findAllADUsers, IPingLdap pingLdap
            ,RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager
            ,SignInManager<ApplicationUser> signInManager, IGenericRepository<CompanyUser> companyUserRepository
            , IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _encriptdescriptStringRepository = encriptdescriptStringRepository;
            _pingLdap = pingLdap;
            _context = context;
            _findAllADUsers = findAllADUsers;
            _domainRepository = domainRepository;
            _companyUserRepository = companyUserRepository;
        }


        //[Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            try
            {
                DomainUserViewModel model = new DomainUserViewModel();
                ApplicationUser au = new ApplicationUser();
                au.Mobile = model.Mobile;
                au.FirstName = model.FirstName;
                au.LastName = model.LastName;
                au.UserName = model.UserName;
                au.PhoneNumber = model.PhoneNumber;
                au.Id = model.Id;
                return View(model);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
           
        }



        [HttpGet]
        //[Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            try
            {
                return View(new DomainUserViewModel());

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DomainUserViewModel model)
        {
            try
            {
                model.message = "";
                model.IsSuccess = false;
                bool ping = _pingLdap.Ping(model.DomainId, model.UserName);
                if (ping)
                {
                    List<DomainUser> users = _findAllADUsers.FindAll(model.DomainId, model.UserName);
                    ApplicationUser applicationUser = new ApplicationUser();
                    List<ApplicationUser> applicationUsers = new List<ApplicationUser>();
                    IdentityResult result = new IdentityResult();

                    if (users.Count > 0)
                    {
                        foreach (var user in users)
                        {
                            applicationUser.UserName = user.UserName + "@" + user.dcString.Split("dc=")[1].Replace(",", ".") + user.dcString.Split(",dc=")[1];
                            applicationUser.FirstName = user.FirstName;
                            applicationUser.LastName = user.LastName;
                            applicationUser.Email = user.Email;
                            applicationUser.EmailConfirmed = true;
                            applicationUser.PhoneNumberConfirmed = true;
                            applicationUser.PhoneNumber = "-";
                            applicationUser.Mobile = "-";
                            applicationUser.AddedDate = DateTime.Now;
                            applicationUsers.Add(applicationUser);
                        }

                        if (ModelState.IsValid)
                        {


                            byte[] salt = new byte[128 / 8];
                            model.PasswordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: "##@@!?M!?E!@$*64##$$^^",
                         salt: salt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8));

                            foreach (var applicationUser1 in applicationUsers)
                            {
                                try
                                {
                                    result = await _userManager.CreateAsync(applicationUser1, model.PasswordHash);
                                    if (result.Succeeded)
                                    {
                                        //if (await _roleManager.FindByNameAsync(model.RoleType.ToString()) == null)
                                        //{

                                        //    await _roleManager.CreateAsync(new ApplicationRole
                                        //    {
                                        //        Name = model.RoleType.ToString(),
                                        //        NormalizedName = model.RoleType.ToString().ToUpper()
                                        //    });
                                        //}

                                        //await _userManager.AddToRoleAsync(applicationUser, model.RoleType.ToString());


                                        var systemRoles = _roleManager.Roles.ToList().Where(x => x.Id == model.RoleId);

                                        var roles = systemRoles.Select(x => x.Name).FirstOrDefault();
                                        if (roles !=null)
                                        {
                                            await _userManager.AddToRoleAsync(applicationUser1, roles.ToString());

                                        }

                                        if (Request.Form.Keys.Contains("SaveAndReturn"))
                                        {
                                            model.IsSuccess = true;
                                            model.message = "Sucsses";
                                            return View(model);
                                        }

                                    }

                                    else
                                    {
                                        var errors = result.Errors.ToList();
                                        if (errors.Count > 0)
                                        {
                                            foreach (var error in errors)
                                            {
                                                AddErrors(result);
                                            }
                                        }

                                        else
                                        {
                                            model.IsSuccess = false;
                                            model.message = "Error";
                                            return View(model);

                                        }
                                    }
                                }
                                catch (Exception e)
                                {

                                    var a = e.Message;
                                }

                            }

                        }
                    }

                    else
                    {
                        var errors = result.Errors.ToList();
                        if (errors.Count > 0)
                        {
                            foreach (var error in errors)
                            {
                                AddErrors(result);
                            }
                        }

                        else
                        {
                            model.IsSuccess = false;
                            model.message = "Error";
                            return View(model);

                        }
                    }

                }

                else
                {

                    ModelState.AddModelError("", "دامین غیر فعال است.!");

                }


                return View(model);
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
                    // ModelState.AddModelError(string.Empty, error.Description);

                    if (error.Description.Contains("is already taken"))
                    {
                        ModelState.AddModelError("", "نام کاربری وارد شده تکراری است.!");
                    }
                }

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
            }
        }


        public ActionResult DomainSetting_ValueMapper(int[] values)
        {
            try
            {
                var indices = new List<int>();

                if (values != null && values.Any())
                {
                    var index = 0;

                    foreach (var woak in TagHelper_DomainSetting())
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


        public IEnumerable<DomainSetting> TagHelper_DomainSetting()
        {
            try
            {
                return _domainRepository.GetAll();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }


        public ActionResult DomainSetting_Read([DataSourceRequest] Kendo.Mvc.UI.DataSourceRequest request)
        {
            try
            {
                return Json(_domainRepository.GetAll().ToList().ToDataSourceResult(request));
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
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
    }
}