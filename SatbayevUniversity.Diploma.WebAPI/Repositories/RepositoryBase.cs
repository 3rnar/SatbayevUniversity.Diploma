using Dapper;
using Microsoft.Extensions.Options;
using SatbayevUniversity.Diploma.WebAPI.Repositories.Interfaces;
using SatbayevUniversity.Diploma.WebAPI.Utilities;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SatbayevUniversity.Diploma.WebAPI.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected string connectionString = null;

        public RepositoryBase(IOptionsMonitor<DBConfig> optionsAccessor)
        {
            if (connectionString == null)
            {
                connectionString = optionsAccessor.CurrentValue.ConnectionString;
            }
        }

        public virtual void Insert(T entity)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns);
            var stringOfParameters = string.Join(", ", columns.Select(e => "@" + e));
            var query = $"insert into {typeof(T).Name}s ({stringOfColumns}) values ({stringOfParameters})";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(query, entity);
            }
        }

        public virtual void Delete(T entity)
        {
            var query = $"delete from {typeof(T).Name}s where Id = @Id";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(query, entity);
            }
        }

        public virtual void Update(T entity)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns.Select(e => $"{e} = @{e}"));
            var query = $"update {typeof(T).Name}s set {stringOfColumns} where Id = @Id";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(query, entity);
            }
        }

        public virtual IEnumerable<T> Query(string where = null)
        {
            var query = $"select * from {typeof(T).Name.ToLower()}s ";

            if (!string.IsNullOrWhiteSpace(where))
                query += where;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<T>(query);
            }
        }

        public virtual T SingleQuery(int id)
        {

            var query = $"SELECT * FROM {typeof(T).Name.ToLower()}s WHERE Id = @Id";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<T>(query, new { Id = id }).SingleOrDefault();
            }
        }

        private IEnumerable<string> GetColumns()
        {
            return typeof(T)
                    .GetProperties()
                    .Where(e => e.Name != "Id" && !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name);
        }

        protected IDbConnection GetConn()
        {
            var conn = new SqlConnection(connectionString);
            return new ProfiledDbConnection(conn, MiniProfiler.Current);
            //return new SqlConnection(ConfigurationManager.ConnectionStrings[connection].ConnectionString);
        }
    }
}
