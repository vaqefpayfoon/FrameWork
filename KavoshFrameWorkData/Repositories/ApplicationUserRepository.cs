using KavoshFrameWorkCore.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KavoshFrameWorkData.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        public KavoshFrameWorkCore.KavoshFrameWorkContext context;
        public DbSet<ApplicationUser> dbSet;
        string errorMessage = string.Empty;
        public ApplicationUserRepository(KavoshFrameWorkCore.KavoshFrameWorkContext context)
        {
            this.context = context;
            this.dbSet = context.Set<ApplicationUser>();
        }
        public async Task<IEnumerable<ApplicationUser>> GetAsync(
            Expression<Func<ApplicationUser, bool>> filter = null,
            Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<ApplicationUser> query = dbSet;
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

        public IQueryable<ApplicationUser> GetAsQueryable(
           Expression<Func<ApplicationUser, bool>> filter = null,
           string includeProperties = "")
        {
            try
            {
                IQueryable<ApplicationUser> query = dbSet;
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

        public IEnumerable<IGrouping<object, ApplicationUser>> GetAsQueryableGroupBy(Func<ApplicationUser, object> group, Expression<Func<ApplicationUser, bool>> filter = null, string includeProperties = "")
        {
            try
            {
                var query = dbSet;
                IEnumerable<IGrouping<object, ApplicationUser>> groupResult = null;
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

        public async Task<ApplicationUser> GetByIDAsync(object id)
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

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync(string includeProperties = "")
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

        public IEnumerable<ApplicationUser> GetAll(string includeProperties = "")
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

        public async Task<int> InsertAsync(ApplicationUser entity)
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

        public int Insert(ApplicationUser entity)
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
                ApplicationUser entityToDelete = dbSet.Find(id);
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
                ApplicationUser entityToDelete = dbSet.Find(id);
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

        public async Task<int> DeleteAsync(ApplicationUser entityToDelete)
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
             Expression<Func<ApplicationUser, bool>> filter = null)
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

        public async Task<int> UpdateAsync(ApplicationUser entityToUpdate)
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

        public async Task<int> InsertAllAsync(List<ApplicationUser> entities)
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
    }
}
