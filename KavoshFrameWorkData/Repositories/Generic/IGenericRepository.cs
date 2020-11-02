using KavoshFrameWorkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KavoshFrameWorkData.Repositories.Generic
{
    public interface IGenericRepository<TEntity> where TEntity : BaseBaseEntity
    {
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "");

        IQueryable<TEntity> GetAsQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "");
       IEnumerable<IGrouping<object, TEntity>> GetAsQueryableGroupBy(
            Func<TEntity, object> group, Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        Task<IEnumerable<TEntity>> GetAllAsync(string includeProperties = "");
        IEnumerable<TEntity> GetAll(string includeProperties = "");
        Task<TEntity> GetByIDAsync(object id);
        Task<int> InsertAsync(TEntity entity);
        Task<int> DeleteAsync(object id);
        Task<int> DeleteAllAsync(
             Expression<Func<TEntity, bool>> filter = null);
        Task<int> DeleteAsync(TEntity entityToDelete);
        Task<int> UpdateAsync(TEntity entityToUpdate);
        Task<int> InsertAllAsync(List<TEntity> entities);
        Task<int> RemoveAsync(object id);
        int Insert(TEntity entity);
        int Update(TEntity entityToUpdate);
        void DetachRepository(TEntity updated, int entryId);
        object PortfoJoin(int id);
    }
}
