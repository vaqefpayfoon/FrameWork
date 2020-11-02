using Microsoft.AspNetCore.Mvc;
using KavoshFrameWorkWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CVBuilderCore.ViewComponents
{
    public class BreadCrumbViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(AdminBreadCrumbArgsViewModel args)
        {          
            var model = new List<AdminBreadCrumbViewModel>();
            var editItem = new AdminBreadCrumbViewModel();
            var listItem = new AdminBreadCrumbViewModel { Url = $"/{args.Controller}", Title = $"لیست {args.EntityTitle} ها" };
            var createItem = new AdminBreadCrumbViewModel { Url = $"/{args.Controller}/Create", Title = $"درج اطلاعات {args.EntityTitle} جدید" };
            if (Url.ActionContext.ActionDescriptor.RouteValues.Values.FirstOrDefault() =="ChangePassword")
            {
                editItem = new AdminBreadCrumbViewModel { Url = $"/{args.Controller}/ChangePassword/{args.Id}", Title = $"ویرایش اطلاعات {args.EntityTitle}" };
            }
            else
            {
                editItem = new AdminBreadCrumbViewModel { Url = $"/{args.Controller}/Edit/{args.Id}", Title = $"ویرایش اطلاعات {args.EntityTitle}" };
            }

            switch (args.PageType)
            {
                case AdminPageType.List:
                    if (listItem.Url == "/Logs" || listItem.Url == "/ApplicationRole")
                    {
                        listItem.IsActive = true;
                        model.Add(listItem);
                        break;
                    }
                    else
                    {
                        listItem.IsActive = true;
                        model.Add(listItem);
                        model.Add(createItem);
                        break;
                    }
                   
                case AdminPageType.Create:
                    model.Add(listItem);
                    createItem.IsActive = true;
                    model.Add(createItem);
                    break;
                case AdminPageType.Edit:
                    if (listItem.Url == "/ApplicationUser")
                    {
                        editItem.IsActive = true;
                        model.Add(editItem);
                        break;
                    }
                    else
                    {
                        model.Add(listItem);
                        editItem.IsActive = true;
                        model.Add(editItem);
                        break;
                    }
                default:
                    break;
            }

            return View(model);
        }
    }
}
