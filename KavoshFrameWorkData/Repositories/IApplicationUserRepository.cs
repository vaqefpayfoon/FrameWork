using KavoshFrameWorkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KavoshFrameWorkData.Repositories
{
    public interface IApplicationUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAsync(
    Expression<Func<ApplicationUser, bool>> filter = null,
    Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>> orderBy = null,
    string includeProperties = "");

        IQueryable<ApplicationUser> GetAsQueryable(
            Expression<Func<ApplicationUser, bool>> filter = null,
            string includeProperties = "");
        IEnumerable<IGrouping<object, ApplicationUser>> GetAsQueryableGroupBy(
             Func<ApplicationUser, object> group, Expression<Func<ApplicationUser, bool>> filter = null, string includeProperties = "");
        Task<IEnumerable<ApplicationUser>> GetAllAsync(string includeProperties = "");
        IEnumerable<ApplicationUser> GetAll(string includeProperties = "");
        Task<ApplicationUser> GetByIDAsync(object id);
        Task<int> InsertAsync(ApplicationUser entity);
        Task<int> DeleteAsync(object id);
        Task<int> DeleteAllAsync(
             Expression<Func<ApplicationUser, bool>> filter = null);
        Task<int> DeleteAsync(ApplicationUser entityToDelete);
        Task<int> UpdateAsync(ApplicationUser entityToUpdate);
        Task<int> InsertAllAsync(List<ApplicationUser> entities);
        Task<int> RemoveAsync(object id);
        int Insert(ApplicationUser entity);
    }
}
