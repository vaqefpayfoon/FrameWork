using KavoshFrameWorkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KavoshFrameWorkData.Repositories.Generic
{
    public interface IPortfoRepository
    {
        IEnumerable<IGrouping<object, CompanyShareholder>> GetAsQueryableGroupBy(
            Func<CompanyShareholder, object> group, Expression<Func<CompanyShareholder, bool>> filter = null, string includeProperties = "");
    }
}
