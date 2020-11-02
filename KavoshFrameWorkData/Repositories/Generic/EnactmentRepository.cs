using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using KavoshFrameWorkCore;
using KavoshFrameWorkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KavoshFrameWorkData.Repositories.Generic
{
    public class EnactmentRepository : IEnactmentRepository
    {
        KavoshFrameWorkContext context;
        public DbSet<Enactment> dbSet;
        public DbSet<EnactmentDetail> dbSet2;
        string errorMessage = string.Empty;
        public EnactmentRepository(KavoshFrameWorkContext rayaPardazContext)
        {
            context = rayaPardazContext;
            this.dbSet = context.Set<Enactment>();
            this.dbSet2 = context.Set<EnactmentDetail>();
        }

        public async Task<IEnumerable<Enactment>> Enactments()
        {
            return await dbSet.Where(x => !x.IsDeleted).Include("Company").ToListAsync();
        }
        public async Task<IEnumerable<Enactment>> Enactments(Expression<Func<Enactment, bool>> filter = null)
        {
            return await dbSet.Where(x => !x.IsDeleted).Include("Company").Where(filter).ToListAsync();
        }
        public async Task<IEnumerable<EnactmentDetail>> EnactmentDetails()
        {
            return await dbSet2.Where(x => !x.IsDeleted).Include("Enactment").ToListAsync();
        }
        public async Task<IEnumerable<EnactmentDetail>> EnactmentDetails(Expression<Func<EnactmentDetail, bool>> filter = null)
        {
            return await dbSet2.Where(x => !x.IsDeleted).Include("Enactment").Where(filter).ToListAsync();
        }
        public async Task<int> InsertAsync(Enactment entity)
        {
            try
            {
                entity.AddedDate = DateTime.Now;

                dbSet.Add(entity);
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<Enactment> GetByIDAsync(int id)
        {
            return await dbSet.Include("EnactmentDetails").FirstOrDefaultAsync(find => find.Id == id);
        }
        public int Insert(Enactment entity)
        {
            try
            {
                entity.AddedDate = DateTime.Now;

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
                Enactment entityToDelete = dbSet.Find(id);
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
                Enactment entityToDelete = dbSet.Find(id);
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
        public async Task<int> DeleteAsync(Enactment entityToDelete)
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
        public async Task<int> UpdateAsync(Enactment entityToUpdate)
        {
            try
            {
                entityToUpdate.LastModifiedDate = DateTime.Now;
                context.Update(entityToUpdate);
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<int> DeleteDetailAsync(object id)
        {
            try
            {
                EnactmentDetail entityToDelete = dbSet2.Find(id);
                entityToDelete.IsDeleted = true;
                entityToDelete.DeleteDate = DateTime.Now;
                //dbSet2.Remove(entityToDelete);
                context.Remove(entityToDelete);
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<int> DeleteDetailAsync(EnactmentDetail entityToDelete)
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
    }
}
