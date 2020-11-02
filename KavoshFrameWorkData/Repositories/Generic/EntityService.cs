using KavoshFrameWorkCore;
using KavoshFrameWorkCore.Dto;
using KavoshFrameWorkCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KavoshFrameWorkData.Repositories.Generic
{
    public class EntityService : IEntityService
    {
        private readonly KavoshFrameWorkContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public EntityService(KavoshFrameWorkContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public IEnumerable<SelectListItem> GetListOfAccessibleEntities()
        {
            //try
            {
                var list = typeof(IAccessible).GetTypeInfo().Assembly
              .GetTypes()
              .Where(x => !x.GetTypeInfo().IsAbstract &&
              x.GetTypeInfo().ImplementedInterfaces.Any(i => i == typeof(IAccessible)) &&
              x.GetTypeInfo().ImplementedInterfaces.Any(i => i != typeof(IAdminEntity)) &&
              !x.GetTypeInfo().IsSubclassOf(typeof(Media)))
              .Select(x => new SelectListItem
              {
                  Text = x.GetCustomAttribute<System.ComponentModel.DisplayNameAttribute>(false)?.DisplayName,
                  Value = x.Name
              });

              return list.Where(x => x.Text != null);
            }
            //catch (Exception e)
            //{
            //    Log.Error(e, e.Message);
            //    return null;
            //}
        }

        public async Task<List<RoleFormActionAssignmentDto>> GetListOfUserAccessibleModules(string username)
        {
            try
            {
                var accessibleEntities = GetListOfAccessibleEntities();

                var user = await userManager.FindByNameAsync(username);

                //var currRoles = context.Set<RoleFormActionAssignment>().Join(context.Set<ApplicationRole>(),
                //    a => a.RoleId, b => b.Id, (a, b) => new { roleFormAssigns = a, roles = b }).Join(context.Set<SystemForm>().Where(a => GetListOfAccessibleEntities().Select(woak => woak.Value).Contains(a.EntityName)), a => a.roleFormAssigns.SystemFormId, b => b.Id, (a, b) => new { allRoles = a, systemForms = b });

                var currRoles = context.Set<ApplicationUser>().Where(woak => woak.Id == user.Id).Join(context.Set<ApplicationRole>(),
                    a => a.RoleId, b => b.Id, (a, b) => new { roleFormCurrUser = a, roles = b }).Join(context.Set<RoleFormActionAssignment>(), a => a.roles.Id, b=> b.RoleId, (a,b) => new { role2Forms = a , roleFormAssigns = b} ).Join(context.Set<SystemForm>().Where(a => GetListOfAccessibleEntities().Select(woak => woak.Value).Contains(a.EntityName)), a => a.roleFormAssigns.SystemFormId, b => b.Id, (a, b) => new { allRoles = a, systemForms = b });

                List <RoleFormActionAssignmentDto> res = currRoles.Select(x => new RoleFormActionAssignmentDto
                {
                    Id = x.allRoles.roleFormAssigns.Id,
                    EntityName = x.systemForms.EntityName,
                    SystemAction = x.allRoles.roleFormAssigns.SystemAction,
                    Priority = x.systemForms.Priority,
                    Title = x.systemForms.Title,
                    ParentTitle = x.systemForms.ParentTitle,
                    ListOnly = x.systemForms.ListOnly,
                }).ToList();
                return res;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public async Task<SystemAction> GetUserAllowedActionFor(string username, string module)
        {
            try
            {
                var list = await GetListOfUserAccessibleModules(username);

                var action = list.Where(x => x.EntityName.ToUpper() == module.ToUpper()).Select(x => x.SystemAction).FirstOrDefault();

                return action;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return SystemAction.None;
            }
        }

        public async Task<SystemAction> GetUserAllowedActionFor(string username, Type moduleType)
        {
            try
            {
                var module = moduleType.ToString().Split('.').LastOrDefault();
                var list = await GetListOfUserAccessibleModules(username);

                var action = list.Where(x => x.EntityName.ToUpper() == module.ToUpper()).Select(x => x.SystemAction).FirstOrDefault();

                return action;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return SystemAction.None;
            }
        }
    }
}
