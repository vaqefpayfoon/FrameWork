using KavoshFrameWorkCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.ViewComponents
{
    public class EmailProfileViewComponent : ViewComponent
    {
        KavoshFrameWorkCore.KavoshFrameWorkContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public EmailProfileViewComponent(KavoshFrameWorkCore.KavoshFrameWorkContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var user = await userManager.GetUserAsync((ClaimsPrincipal)User);
            var userId = user?.Id;
            var email = user?.Email;
            return View("Default", email);


        }
    }
}
