using KavoshFrameWorkCore.Models;
using System.Collections.Generic;

namespace KavoshFrameWorkData.Repositories
{
    public interface IDepartmentQueries
    {
        IEnumerable<LogModel> GetAllLogs();
    }
}
