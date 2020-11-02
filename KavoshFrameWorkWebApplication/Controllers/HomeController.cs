using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;

namespace KavoshFrameWorkWebApplication.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(UserManager<ApplicationUser> userManager)
        {

            this._userManager = userManager;
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
    }
}
