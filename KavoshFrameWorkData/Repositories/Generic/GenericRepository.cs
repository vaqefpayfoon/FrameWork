using Microsoft.EntityFrameworkCore;
using KavoshFrameWorkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Serilog;

namespace KavoshFrameWorkData.Repositories.Generic
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseBaseEntity
    {
        public KavoshFrameWorkCore.KavoshFrameWorkContext context;
        public DbSet<TEntity> dbSet;
        string errorMessage = string.Empty;
        public GenericRepository(KavoshFrameWorkCore.KavoshFrameWorkContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = dbSet;
                query = query.Where(x => !x.IsDeleted);

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await query.ToListAsync();
                }
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public IEnumerable<TEntity> Get(
       Expression<Func<TEntity, bool>> filter = null,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
       string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            query = query.Where(x => !x.IsDeleted);

            if (filter != null)
            {
                query = query.Where(filter).AsNoTracking();
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty).AsNoTracking();
            }

            if (orderBy != null)
            {
                return orderBy(query.AsNoTracking()).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public IQueryable<TEntity> GetAsQueryable(
           Expression<Func<TEntity, bool>> filter = null,
           string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = dbSet;
                query = query.Where(x => !x.IsDeleted);
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                return query;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public IEnumerable<IGrouping<object, TEntity>> GetAsQueryableGroupBy(Func<TEntity, object> group, Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            try
            {
                var query = dbSet;
                IEnumerable<IGrouping<object, TEntity>> groupResult = null;
                if (filter != null)
                {
                    groupResult = query.Where(filter).GroupBy(group);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    groupResult = query.Include(includeProperty).GroupBy(group);
                }

                return groupResult;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public async Task<TEntity> GetByIDAsync(object id)
        {
            try
            {
                return await dbSet.FindAsync(id);

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(string includeProperties = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(includeProperties))
                    return await dbSet.Where(x => !x.IsDeleted).Include(includeProperties).ToListAsync();
                else
                    return await dbSet.Where(x => !x.IsDeleted).ToListAsync();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public IEnumerable<TEntity> GetAll(string includeProperties = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(includeProperties))
                    return dbSet.Where(x => !x.IsDeleted).Include(includeProperties).ToList();
                else
                {
                    var item = dbSet.Where(x => !x.IsDeleted).ToList();
                    return item;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            try
            {
                entity.AddedDate = DateTime.Now;

                dbSet.Add(entity);
                return await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return -1;
            }
        }

        public int Insert(TEntity entity)
        {
            try
            {
                entity.AddedDate = DateTime.Now;

                dbSet.Add(entity);
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return -1;
            }
        }

        public async Task<int> RemoveAsync(object id)
        {
            try
            {
                TEntity entityToDelete = dbSet.Find(id);
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
                return await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return -1;
            }
        }

        public async Task<int> DeleteAsync(object id)
        {
            try
            {
                TEntity entityToDelete = dbSet.Find(id);
                entityToDelete.IsDeleted = true;
                entityToDelete.DeleteDate = DateTime.Now;
                context.Update(entityToDelete);
                return await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return -1;
            }
        }

        public async Task<int> DeleteAsync(TEntity entityToDelete)
        {
            try
            {
                entityToDelete.IsDeleted = true;
                entityToDelete.DeleteDate = DateTime.Now;
                context.Update(entityToDelete);
                return await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return -1;
            }
        }

        public async Task<int> DeleteAllAsync(
             Expression<Func<TEntity, bool>> filter = null)
        {
            try
            {
                var itemsToBeDeleted = dbSet.Where(filter);
                dbSet.RemoveRange(itemsToBeDeleted);
                return await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return -1;
            }
        }

        public async Task<int> UpdateAsync(TEntity entityToUpdate)
        {
            try
            {
                context.Entry(entityToUpdate).State = EntityState.Modified;

                entityToUpdate.LastModifiedDate = DateTime.Now;
                context.Update(entityToUpdate);
                return await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return -1;
            }
        }

        public async Task<int> InsertAllAsync(List<TEntity> entities)
        {
            try
            {
                dbSet.AddRange(entities);
                return await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return -1;
            }
        }

        public int Update(TEntity entityToUpdate)
        {
            try
            {
                entityToUpdate.LastModifiedDate = DateTime.Now;
                context.Update(entityToUpdate);
                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public void DetachRepository(TEntity updated, int entryId)
        {
            var local = context.Set<Company>().Local.FirstOrDefault(entry => entry.Id.Equals(entryId));
            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }
            context.Entry(updated).State = EntityState.Modified;

        }
        public object PortfoJoin(int id)
        {

            var companiesShareholders = context.Set<CompanyShareholder>().Where(x => x.IncludeInFormula).GroupBy(woak => new { woak.CompanyId, woak.ShareholderId }).Select(g => g.OrderByDescending(c => c.Id).FirstOrDefault()).ToList();

            var companies = context.Set<Company>().ToList();

            var shareholders = context.Set<Shareholder>();

            var allPossibleShareholders = context.Set<PortfoShareholderPair>().Where(woak => woak.PortfoId == id).OrderBy(p => p.Row).Join(companiesShareholders, a => new { a.CompanyId, a.ShareholderId }, b => new { b.CompanyId, b.ShareholderId }, (a, b) => new { portfo = a, companiesShare = b }).Join(companies, a => a.companiesShare.CompanyId, b => b.Id, (a, b) => new { companiesShare = a, companies = b }).Join(shareholders, a => a.companiesShare.companiesShare.ShareholderId, b => b.Id, (a, b) => new { companiesShare2 = a, shares = b }).ToList();




            return allPossibleShareholders;

        }

    }
}
