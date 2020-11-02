using KavoshFrameWorkCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Linq;

namespace KavoshFrameWorkCore.Extensions
{
    public static class DbContextExtension
    {
        public static IQueryable<T> GetTemporal<T>(this DbContext context, DateTime dt) where T : class, ITemporal
        {
            var tableName = context.Model.FindEntityType(typeof(T)).GetTableName();
            var schema = context.Model.FindEntityType(typeof(T)).GetSchema();

            return context.Set<T>().FromSqlRaw($"SELECT * FROM {schema}.{tableName} FOR SYSTEM_TIME AS OF {{0}}", dt.ToUniversalTime());
            
        }

        public static IQueryable<T> GetTemporal<T>(this DbSet<T> table, DateTime dt) where T : class, ITemporal
        {
            var dbContext = table.GetDbContext();

            var model = dbContext.Model;
            var entityTypes = model.GetEntityTypes();
            var entityType = entityTypes.First(t => t.ClrType == typeof(T));
            var tableName = entityType.GetAnnotation("Relational:TableName").Value.ToString();
            var schema = entityType.GetAnnotations().FirstOrDefault(e => e.Name == "Relational:Schema")?.Value.ToString() ?? string.Empty;

            return table.FromSqlRaw($"SELECT * FROM {schema}.{tableName} FOR SYSTEM_TIME AS OF {{0}}", dt.ToUniversalTime());


        }

        public static DbContext GetDbContext<T>(this DbSet<T> dbSet) where T : class
        {
            var infrastructure = dbSet as IInfrastructure<IServiceProvider>;
            var serviceProvider = infrastructure.Instance;
            var currentDbContext = serviceProvider.GetService(typeof(ICurrentDbContext))
                                       as ICurrentDbContext;
            return currentDbContext.Context;
        }

        public static void CreateTemporal(this DbContext dbContext)
        {
            var properties = (from p in dbContext.GetType().GetProperties()
                              where p.PropertyType.IsGenericType
                              && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)
                              let entityType = p.PropertyType.GetGenericArguments().First()
                              where typeof(ITemporal).IsAssignableFrom(entityType)
                              select new { name = p.Name, type = p.PropertyType.GetGenericArguments().First() }
                            ).ToList();
            dbContext.Database.ExecuteSqlCommand(@"IF  SCHEMA_ID('history') is null EXECUTE('CREATE SCHEMA [history]')");

            foreach (var dbset in properties)
            {
                var mapping = dbContext.Model.FindEntityType(dbset.type).GetTableName();
                var dbsetName = mapping;

                dbContext.Database.ExecuteSqlCommand(@"IF (EXISTS (SELECT * 
                                                        FROM INFORMATION_SCHEMA.TABLES 
                                                        WHERE TABLE_SCHEMA = 'history' 
                                                        AND  TABLE_NAME = '" + dbsetName + @"'))
                                                            BEGIN
                                                            print '1'
                                                            END
                                                            ELSE
                                                                BEGIN
                                                            ALTER TABLE " + dbsetName + @" ADD 
                                                             SysStartTime datetime2(0) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
                                                             SysEndTime datetime2(0) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
                                                             PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime);
                                                             ALTER TABLE " + dbsetName + @"
                                                             SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = history." + dbsetName + @" ));
                                                             END");


            }
        }

        public static void DistableTemporal(this MigrationBuilder migrationBuilder, string tableName)
        {
            string query = $"IF((SELECT temporal_type FROM sys.tables WHERE  object_id = OBJECT_ID('{tableName}', 'u')) IS NOT NULL AND (SELECT temporal_type FROM sys.tables WHERE  object_id = OBJECT_ID('{tableName}', 'u')) <> 0 AND (SELECT TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES WHERE CONCAT(TABLE_SCHEMA,'.',TABLE_NAME) = '{tableName}') IS NOT NULL) BEGIN ALTER TABLE {tableName} SET (SYSTEM_VERSIONING = OFF) END;";
            migrationBuilder.Sql(query);
        }
    }
}
