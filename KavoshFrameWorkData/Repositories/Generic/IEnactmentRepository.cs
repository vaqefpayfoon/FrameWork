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
    public interface IEnactmentRepository 
    {
        Task<IEnumerable<Enactment>> Enactments();
        Task<IEnumerable<Enactment>> Enactments(Expression<Func<Enactment, bool>> filter = null);
        Task<IEnumerable<EnactmentDetail>> EnactmentDetails();
        Task<IEnumerable<EnactmentDetail>> EnactmentDetails(Expression<Func<EnactmentDetail, bool>> filter = null);
        Task<Enactment> GetByIDAsync(int id);
        Task<int> InsertAsync(Enactment entity);
        Task<int> DeleteAsync(object id);
        Task<int> DeleteAsync(Enactment entityToDelete);
        Task<int> DeleteDetailAsync(object id);
        Task<int> DeleteDetailAsync(EnactmentDetail entityToDelete);
        Task<int> UpdateAsync(Enactment entityToUpdate);
        Task<int> RemoveAsync(object id);
        int Insert(Enactment entity);
    }
}
