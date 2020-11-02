using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using KavoshFrameWorkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KavoshFrameWorkData.Repositories.Generic
{
    public class TreeRepository<TEntity> : ITreeRepository<TEntity> where TEntity : BaseTree
    {
        public KavoshFrameWorkCore.KavoshFrameWorkContext context;
        public DbSet<TEntity> dbSet;
        string errorMessage = string.Empty;
        public TreeRepository(KavoshFrameWorkCore.KavoshFrameWorkContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
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

        public IQueryable<TEntity> GetAsQueryable(
           Expression<Func<TEntity, bool>> filter = null,
           string includeProperties = "")
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

        public async Task<TEntity> GetByIDAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(string includeProperties = "")
        {
            if (!string.IsNullOrEmpty(includeProperties))
                return await dbSet.Where(x => !x.IsDeleted).Include(includeProperties).ToListAsync();
            else
                return await dbSet.Where(x => !x.IsDeleted).ToListAsync();

        }

        public IEnumerable<TEntity> GetAll(string includeProperties = "")
        {
            if (!string.IsNullOrEmpty(includeProperties))
                return dbSet.Where(x => !x.IsDeleted).Include(includeProperties).ToList();
            else
                return dbSet.Where(x => !x.IsDeleted).ToList();
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            //try
            {
             
                dbSet.Add(entity);
                return await context.SaveChangesAsync();
            }
            //catch (Exception ex)
            {
                //return -1;
            }
        }

        public int Insert(TEntity entity)
        {
            try
            {

                dbSet.Add(entity);
                return context.SaveChanges();
            }
            catch (Exception ex)
            {
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
            catch (Exception ex)
            {
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
            catch (Exception ex)
            {
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
            catch (Exception ex)
            {
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
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> UpdateAsync(TEntity entityToUpdate)
        {
            //try
            {
                context.Entry(entityToUpdate).State = EntityState.Modified;
                context.Update(entityToUpdate);
                return await context.SaveChangesAsync();
            }
            //catch (Exception ex)
            {
                //return -1;
            }
        }

        public async Task<int> InsertAllAsync(List<TEntity> entities)
        {
            //try
            {
                dbSet.AddRange(entities);
                return await context.SaveChangesAsync();
            }
            //catch (Exception ex)
            {
                //return -1;
            }
        }
        public void DetachRepository(TEntity updated, string entryId)
        {
            var local = context.Set<Company>().Local.FirstOrDefault(entry => entry.Id.Equals(entryId));

            // check if local is not null 
            if (local != null) // I'm using a extension method
            {
                // detach
                context.Entry(local).State = EntityState.Detached;
            }
            // set Modified flag in your entry
            context.Entry(updated).State = EntityState.Modified;

        }

    }
}
