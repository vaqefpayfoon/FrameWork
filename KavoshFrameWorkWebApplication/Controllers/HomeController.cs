using ExcelReader;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkWebApplication.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace KavoshFrameWorkWebApplication.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HomeController(UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            this._userManager = userManager;
        }
        [HttpGet]
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
