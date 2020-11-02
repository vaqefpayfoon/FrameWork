using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using KavoshFrameWorkWebApplication.Models;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace CVBuilderCore.ViewComponents
{
    public class HeaderProfileViewComponent : ViewComponent
    {
        KavoshFrameWorkCore.KavoshFrameWorkContext context;
        public HeaderProfileViewComponent(KavoshFrameWorkCore.KavoshFrameWorkContext context)
        {
            this.context = context;
        }

        public IViewComponentResult Invoke()
        {

            return View("Default", User.Identity.Name);

        }
       
    }
}
