using KavoshFrameWorkCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KavoshFrameWorkData.Repositories.Generic
{
    public class PortfoRepository : IPortfoRepository
    {
        public KavoshFrameWorkCore.KavoshFrameWorkContext context;
        public DbSet<CompanyShareholder> dbSet;
        string errorMessage = string.Empty;
        public PortfoRepository(KavoshFrameWorkCore.KavoshFrameWorkContext context)
        {
            this.context = context;
            this.dbSet = context.Set<CompanyShareholder>();
        }
        public IEnumerable<IGrouping<object, CompanyShareholder>> GetAsQueryableGroupBy(Func<CompanyShareholder, object> group, Expression<Func<CompanyShareholder, bool>> filter = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }
    }
}
