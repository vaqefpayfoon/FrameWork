using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KavoshFrameWorkWebApplication.Models;
using KavoshFrameWorkData.Repositories.Generic;
using KavoshFrameWorkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CVBuilderCore.ViewComponents
{
    public class SimpleDropDownListViewComponent : ViewComponent
    {
        KavoshFrameWorkCore.KavoshFrameWorkContext context;
        public SimpleDropDownListViewComponent(KavoshFrameWorkCore.KavoshFrameWorkContext context)
        {
            this.context = context;
        }

        public IViewComponentResult Invoke(string entity, object id, List<int> ids = null, int? parentId = null)
        {
            var systemTypesAssembly = typeof(BaseEntity).GetTypeInfo().Assembly;
            var type = systemTypesAssembly.GetTypes().Where(t => t.Name == entity).FirstOrDefault();
            dynamic items = Activator.CreateInstance(type);

            var d1 = typeof(GenericRepository<>);
            Type[] typeArgs = { type };
            var makeme = d1.MakeGenericType(typeArgs);
            dynamic _service = Activator.CreateInstance(makeme, context);

            items = _service.GetAll();
            var model = new List<KeyTitleViewModel>();
            foreach (var item in items)
            {
                var temp = new KeyTitleViewModel
                {
                    Title = item.Title,
                    Id = item.Id,
                };
                if (id != null)
                    temp.IsSelected = id != null && item.Id.ToString() == id.ToString();
                else if (ids != null)
                    temp.IsSelected = ids.Contains(item.Id);

                model.Add(temp);
            }
            return View(model);
        }
    }
   
    public class UserCompanyDropDownListViewComponent : ViewComponent
    {
        KavoshFrameWorkCore.KavoshFrameWorkContext context;
        public UserCompanyDropDownListViewComponent(KavoshFrameWorkCore.KavoshFrameWorkContext context)
        {
            this.context = context;
        }

        public IViewComponentResult Invoke(string id)
        {
            var username = User.Identity.Name;
            var isAdmin = User.IsInRole("Admin");
            ApplicationUser currUser = context.Set<ApplicationUser>().FirstOrDefault(woak => woak.UserName.Equals(username));
            List<ApplicationUser> companyIds = null;

            companyIds = context.Set<ApplicationUser>().ToList();

            var model = companyIds.Select(x => new KeyTextViewModel
            {
                    Id = x.Id,
                    Text = x.UserName,
                    IsSelected = x.Id == id

                }).ToList();
            return View(model);
        }
    }

 

    public class DomainDropDownListViewComponent : ViewComponent
    {
        KavoshFrameWorkCore.KavoshFrameWorkContext context;
        public DomainDropDownListViewComponent(KavoshFrameWorkCore.KavoshFrameWorkContext context)
        {
            this.context = context;
        }
        public IViewComponentResult Invoke(int? id, int DomainID)
        {

            var username = User.Identity.Name;
            var isAdmin = User.IsInRole("Admin");
            var domainIds = context.Set<DomainSetting>()
                .Include(x => x.Server)
                .Select(x => x.Id).ToList();

            var model = this.context.Set<DomainSetting>().Where(x => domainIds.Contains(x.Id) || isAdmin)
                .Select(x => new KeyTitleViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    IsSelected = x.Id == id

                }).ToList();
            return View(model);
        }
    }



    public class RolesDropDownListViewComponent : ViewComponent
    {
        KavoshFrameWorkCore.KavoshFrameWorkContext context;
        public RolesDropDownListViewComponent(KavoshFrameWorkCore.KavoshFrameWorkContext context)
        {
            this.context = context;
        }
        public IViewComponentResult Invoke(string id)
        {
            var hiddenRoles = new List<string>();
            if (string.IsNullOrEmpty(id))
                hiddenRoles = context.Set<RoleFormActionAssignment>().Select(x => x.RoleId).ToList();

            var model = this.context.Set<ApplicationRole>().Where(x => !hiddenRoles.Contains(x.Id))
                .Select(x => new KeyTextViewModel
                {
                    Id = x.Id,
                    Text = x.Name,
                    IsSelected = x.Id == id
                }).ToList();
            return View(model);
        }
    }


    public class RoleDropDownListViewComponent : ViewComponent
    {
        KavoshFrameWorkCore.KavoshFrameWorkContext context;
        public RoleDropDownListViewComponent(KavoshFrameWorkCore.KavoshFrameWorkContext context)
        {
            this.context = context;
        }
        public IViewComponentResult Invoke(string id)
        {
            var Roles = new List<string>();
            //  if (string.IsNullOrEmpty(id))

            Roles = context.Set<ApplicationRole>().Select(x => x.Id).ToList();

            var model = this.context.Set<ApplicationRole>().Where(x => Roles.Contains(x.Id))
                .Select(x => new KeyTextViewModel
                {
                    Id = x.Id,
                    Text = x.Name,
                    IsSelected = x.Id == id
                }).ToList();
            return View(model);
        }
    }

}
