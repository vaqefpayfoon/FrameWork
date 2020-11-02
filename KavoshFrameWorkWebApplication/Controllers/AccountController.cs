using DNTCaptcha.Core;
using DNTCaptcha.Core.Providers;
using KavoshFrameWorkCore;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkData.Repositories.Ldap;
using KavoshFrameWorkWebApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Serilog;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly SignInManager<ApplicationUser> _signInManager;
        private readonly KavoshFrameWorkContext _context;
        IEncriptDescriptString _encriptdescriptStringRepository;
        IFindAllADUsers _findAllADUsers;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
             IFindAllADUsers findAllADUsers,
              IEncriptDescriptString encriptdescriptStringRepository,
            KavoshFrameWorkContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _findAllADUsers = findAllADUsers;
            _encriptdescriptStringRepository = encriptdescriptStringRepository;
        }

        public IActionResult Index()
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                    RedirectToAction("Index", "Home");
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

                ViewData["ReturnUrl"] = returnUrl;
                return View();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
            
        }

        [HttpPost]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(ErrorMessage = "لطفا عبارت امنیتی را درست وارد نمایید.",
                   CaptchaGeneratorLanguage = Language.English,
                   CaptchaGeneratorDisplayMode = DisplayMode.SumOfTwoNumbers)]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            try
            {
                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    if (model.UserName.Contains("@"))
                    {
                        bool authentic = false;
                        List<DomainUser> users = _findAllADUsers.FindAll(model.DomainId, model.UserName);
                        
                        ApplicationUser applicationUser = new ApplicationUser();
                        List<ApplicationUser> applicationUsers = new List<ApplicationUser>();

                        foreach (var user in users)
                        {
                            applicationUser.UserName = user.DisplayName;
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
                        IQueryable<DomainSetting> domainSetting = _context.DomainSetting.Where(w => w.Id == model.DomainId);
                        string cipherText = "";
                        var domain = domainSetting.Select(w => new DomainSetting
                        {
                            UserName = w.UserName,
                            Server = w.Server,
                            Title = w.Title,
                            Password = w.Password
                        }).FirstOrDefault();
                        cipherText = _encriptdescriptStringRepository.DecryptString(domain.Password);
                        string dcString = "";
                        string rootNode = "";
                        string[] arrString;
                        arrString = domain.Title.Split('.');
                        if (arrString.Length == 1)
                        {
                            dcString = "dc=" + domain.Title + "";
                            rootNode = arrString[0];
                        }
                        else
                        {
                            for (int i = 0; i != arrString.Length; i++)
                            {
                                dcString += "dc=" + arrString[i].ToString() + ",";
                            }
                            if (arrString.Length == 3)
                                rootNode = arrString[1].ToString();
                            else if (arrString.Length == 2)
                                rootNode = arrString[0].ToString();
                            dcString = dcString.Substring(0, dcString.Length - 1);

                        }
                        string DomainPath = "LDAP://" + domain.Server + "/" + dcString;
                        DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain.Server + "/" + dcString, model.UserName.Split("@")[0], model.Password);
                        try
                        {
                            object nativeObject = entry.NativeObject;

                            authentic = true;
                            byte[] salt = new byte[128 / 8];
                            model.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                password: "##@@!?M!?E!@$*64##$$^^",
                                 salt: salt,
                                prf: KeyDerivationPrf.HMACSHA1,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 8));
                            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
                            if (result.Succeeded)
                            {

                                return RedirectToLocal(returnUrl);
                            }
                        }
                        catch (Exception e)
                        {

                            ModelState.AddModelError(string.Empty, Resources.Messages.InvalidLoginAttempt);
                            return View(model);
                        }
                    }
                    else
                    {
                        IQueryable<DomainSetting> domainSetting = _context.DomainSetting.Where(w => w.Id == model.DomainId);

                        var domain = domainSetting.Select(w => new DomainSetting
                        {
                            Id = w.Id,
                            Title = w.Title,
                        }).FirstOrDefault();

                        if (domain.Title == "کاربران سیستمی")
                        {
                            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
                            if (result.Succeeded)
                            {
                                return RedirectToLocal(returnUrl);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, Resources.Messages.InvaliDomainAttempt);
                            return View(model);
                        }

                    }


                    ModelState.AddModelError(string.Empty, Resources.Messages.InvalidLoginAttempt);
                    return View(model);
                }

                // If we got this far, something failed, redisplay form
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
        public IActionResult ForgotPassword()
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
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        ApplicationUser user = null;
                        var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, model.Mobile);
                        var url = $"http://172.16.5.10:8800/?PhoneNumber={model.Mobile}&Text=Code:{code}&User=ravabetomomi&Password=ravabet123";
                        try
                        {
                            await new System.Net.Http.HttpClient().GetAsync(url);
                        }
                        catch (Exception)
                        {

                        }
                        return RedirectToAction("ResetPassword", new { user = user.Id });
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError("", ex.Message);
                        return View(model);
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
        [AllowAnonymous]
        public IActionResult ResetPassword(string user = null)
        {
            try
            {
                var model = new ResetPasswordViewModel { UserId = user };
                return View(model);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    ModelState.AddModelError("", "کاربر یافت نشد");
                    return View(model);
                }


                if ((await _userManager.VerifyChangePhoneNumberTokenAsync(user, model.Code, user.Mobile)))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                    if (result.Succeeded)
                    {

                        var res = await _signInManager.PasswordSignInAsync(user, model.NewPassword, true, false);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        result.Errors.ToList().ForEach(err => ModelState.AddModelError("", err.Description));
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
        //// [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();

                return RedirectToAction(nameof(AccountController.Login), "Account");
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        public IActionResult AccessDenied()
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


        #region Helpers
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

        private IActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = e.Message });
            }
        }

        [HttpGet]
        private List<DomainUserViewModel> FindAllADUsers(int id, string userName)// GetAllADUsers(int id, string UseName) 
        {
            try
            {
                string cipherText = "";
                IQueryable<DomainSetting> domainSetting = _context.DomainSetting.Where(w => w.Id == id);

                var domain = domainSetting.Select(w => new DomainSetting
                {
                    UserName = w.UserName,
                    Server = w.Server,
                    Title = w.Title,
                    Password = w.Password
                }).FirstOrDefault();
                string EncryptionKey = "MAKV2SPBNI99212";
                byte[] cipherBytes = Convert.FromBase64String(domain.Password);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                List<DomainUserViewModel> lstADUsers = new List<DomainUserViewModel>();
                DomainUserViewModel objSurveyUsers = new DomainUserViewModel();
                DomainSettingViewModel SelectAll = new DomainSettingViewModel();
                string dcString = "";
                string rootNode = "";
                string[] arrString;
                arrString = domain.Title.Split('.');
                if (arrString.Length == 1)
                {
                    dcString = "dc=" + domain.Title + "";
                    rootNode = arrString[0];
                }
                else
                {
                    for (int i = 0; i != arrString.Length; i++)
                    {
                        dcString += "dc=" + arrString[i].ToString() + ",";
                    }
                    if (arrString.Length == 3)
                        rootNode = arrString[1].ToString();
                    else if (arrString.Length == 2)
                        rootNode = arrString[0].ToString();
                    dcString = dcString.Substring(0, dcString.Length - 1);
                }
                try
                {
                    string DomainPath = "LDAP://" + domain.Server + "/" + dcString;
                    System.DirectoryServices.DirectoryEntry searchRoot = new System.DirectoryServices.DirectoryEntry(DomainPath);
                    searchRoot.Username = domain.UserName;
                    searchRoot.Password = cipherText;
                    DirectorySearcher search = new DirectorySearcher(searchRoot);
                    if (userName == "*")
                    {
                        search.Filter = $"(objectClass=user)";
                    }
                    else
                    {
                        userName = userName.Split("@")[0];
                        search.Filter = $"(samaccountname=*{userName}*)";
                    }

                    search.PropertiesToLoad.Add("samaccountname");
                    search.PropertiesToLoad.Add("mail");
                    search.PropertiesToLoad.Add("usergroup");
                    search.PropertiesToLoad.Add("displayname");//first name
                    search.PropertiesToLoad.Add("givenname");//first name
                    search.PropertiesToLoad.Add("sn");//first name
                    SearchResult resultFetch;


                    SearchResultCollection resultCol = search.FindAll();
                    if (resultCol != null)
                    {
                        for (int counter = 0; counter < resultCol.Count; counter++)
                        {
                            string UserNameEmailString = string.Empty;
                            resultFetch = resultCol[counter];
                            if (resultFetch.Properties.Contains("samaccountname"))
                            {
                                objSurveyUsers = new DomainUserViewModel();
                                if (resultFetch.Properties.Contains("mail"))
                                {
                                    objSurveyUsers.Email = (String)resultFetch.Properties["mail"][0];
                                }
                                else
                                {
                                    //  objSurveyUsers.Email = (String)resultFetch.Properties["samaccountname"][0] + id.ToString() + "@Pointer.com";
                                }

                                if (resultFetch.Properties.Contains("displayname"))
                                {
                                    objSurveyUsers.DisplayName = (String)resultFetch.Properties["displayname"][0];
                                }
                                else
                                {
                                    objSurveyUsers.DisplayName = (String)resultFetch.Properties["samaccountname"][0];
                                }


                                objSurveyUsers.UserName = (String)resultFetch.Properties["samaccountname"][0];

                                if (resultFetch.Properties.Contains("givenname"))
                                {
                                    objSurveyUsers.FirstName = (String)resultFetch.Properties["givenname"][0];
                                }
                                else
                                {
                                    objSurveyUsers.FirstName = (String)resultFetch.Properties["samaccountname"][0];
                                }
                                if (resultFetch.Properties.Contains("sn"))
                                {
                                    objSurveyUsers.LastName = (String)resultFetch.Properties["sn"][0];
                                }
                                else
                                {
                                    objSurveyUsers.LastName = (String)resultFetch.Properties["samaccountname"][0];
                                }
                                objSurveyUsers.dcString = dcString;
                                lstADUsers.Add(objSurveyUsers);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                return lstADUsers;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        #endregion
    }
}