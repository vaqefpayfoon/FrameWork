using KavoshFrameWorkCore.Dto;
using KavoshFrameWorkCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KavoshFrameWorkData.Repositories.Generic
{
    public interface IEntityService
    {
        IEnumerable<SelectListItem> GetListOfAccessibleEntities();
        Task<List<RoleFormActionAssignmentDto>> GetListOfUserAccessibleModules(string username);
        Task<SystemAction> GetUserAllowedActionFor(string username,string module);
        Task<SystemAction> GetUserAllowedActionFor(string username, Type moduleType);


    }
}
