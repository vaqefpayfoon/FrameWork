using Dapper;
using KavoshFrameWorkCore.Models;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace KavoshFrameWorkData.Repositories
{
    public class DepartmentQueries : IDepartmentQueries
    {

        private string _connectionString;
        public DepartmentQueries(IOptions<ConnectionConfig> connectionConfig)
        {
            try
            {
                var connection = connectionConfig.Value;
                string connectionString = connection.ConnectionStringDapper;

                _connectionString = connectionString;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                throw;
            }

        }

        public IEnumerable<LogModel> GetAllLogs()
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    return conn.Query<LogModel>("SELECT  *,JSON_VALUE(LogEvent, '$.Properties.ActionId') AS ActionName ,JSON_VALUE(LogEvent, '$.Properties.ActionName') AS ActionName, JSON_VALUE(LogEvent, '$.Properties.IP') AS IP , JSON_VALUE(LogEvent, '$.Properties.User') AS UserName ,JSON_VALUE(LogEvent, '$.Properties.RequestId') AS RequestId , JSON_VALUE(LogEvent, '$.Properties.RequestPath') AS RequestPath  FROM [dbo].[Logs] where Level = 'Error'").AsEnumerable();
                }
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

    }

}
