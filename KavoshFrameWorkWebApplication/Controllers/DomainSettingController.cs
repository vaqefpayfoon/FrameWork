using AutoMapper;
using KavoshFrameWorkCore;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using KavoshFrameWorkData.Repositories.Ldap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Identity;

namespace KavoshFrameWorkWebApplication.Controllers
{
    public class DomainSettingController : BaseController
    {
        IMapper _mapper;
        IGenericRepository<DomainSetting> _domainRepository;
        IEncriptDescriptString _encriptdescriptStringRepository;
        IPingLdap _pingLdap;
        private readonly KavoshFrameWorkContext _context;

        public DomainSettingController(IGenericRepository<DomainSetting> domainRepository,IEncriptDescriptString encriptdescriptStringRepository,
             IPingLdap pingLdap, IMapper mapper, KavoshFrameWorkContext context, UserManager<ApplicationUser> userManager)
        {
            _domainRepository = domainRepository;
            _encriptdescriptStringRepository = encriptdescriptStringRepository;
            _pingLdap = pingLdap;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            try
            {
                int DomainId = 0;
                string Password = "";
                // var items = _domainRepository.GetAsQueryable(x => HasAccess1(x.Id)).ToList();
                var items = _domainRepository.GetAll().ToList();
                var model = _mapper.Map<IEnumerable<DomainSetting>, IEnumerable<DomainSettingViewModel>>(items);

                DomainSettingViewModel m = new DomainSettingViewModel();

                foreach (var x in model)
                {
                    m.Id = x.Id;
                    m.Server = x.Server;
                    m.Title = x.Title;
                    m.IsActive = x.IsActive;
                    m.Password = x.Password;

                }
                IQueryable<DomainSetting> domainSetting = _context.DomainSetting.Where(w => w.Title != "کاربران سیستمی");
                var domain = domainSetting.Select(w => new DomainSetting
                {
                    Id = w.Id,
                    Server = w.Server,
                    Title = w.Title,
                    UserName = w.UserName,
                    Password = w.Password
                }).FirstOrDefault();
                if (domain != null)
                {
                    string cipherText = "";
                    cipherText = _encriptdescriptStringRepository.DecryptString(domain.Password);
                    DomainId = domain.Id;
                    Password = cipherText;
                    bool ping = _pingLdap.Ping(DomainId, Password);
                    if (ping)
                    {
                        if (m.Title != "کاربران سیستمی")
                        {
                            m.IsActive = true;
                        }
                        foreach (var y in model)
                        {
                            if (y.Title != "کاربران سیستمی")
                            {
                                y.IsActive = m.IsActive;
                            }

                        }
                    }
                    return View(model);
                }
                else
                {
                    return View(model);
                }
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
                return View(new DomainSettingViewModel());

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(DomainSettingViewModel model)
        {
            try
            {
                model.SystemUserId = UserId;

                if (ModelState.IsValid)
                {
                    string clearText = "";

                    var item = _mapper.Map<DomainSettingViewModel, DomainSetting>(model);

                    clearText = _encriptdescriptStringRepository.EncryptString(model.Password);

                    item.Password = clearText;
                    await _domainRepository.InsertAsync(item);

                    ErrorMessage = Resources.Messages.ChangesSavedSuccessfully;
                    if (Request.Form.Keys.Contains("SaveAndReturn"))
                    {
                        return RedirectToAction("Index");
                    }

                    else
                    { return RedirectToAction("Edit", new { id = item.Id }); }

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
                var item = await _domainRepository.GetByIDAsync(id);
                var model = _mapper.Map<DomainSetting, DomainSettingViewModel>(item);
                byte[] salt = new byte[128 / 8];
                item.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: "##@@!?M!?E!@$*64##$$^^",
                         salt: salt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8));
                return View(model);
            }
            catch(Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DomainSettingViewModel model)
        {
            try
            {
                model.SystemUserId = UserId;
                if (ModelState.IsValid)
                {

                    var item = _mapper.Map<DomainSettingViewModel, DomainSetting>(model);

                    var result = await _domainRepository.UpdateAsync(item);

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
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _domainRepository.RemoveAsync(id);

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