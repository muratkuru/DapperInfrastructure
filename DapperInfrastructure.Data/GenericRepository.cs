using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace DapperInfrastructure.Data
{
    public interface IRepository<T> where T : class
    {
        T GetById(object id);

        T GetByQuery(string query, object parameters);

        ICollection<T> GetAll();

        ICollection<T> GetAllByQuery(string query, object parameters);

        long Add(T entity);

        bool Update(T entity);

        bool Delete(T entity);

        bool Delete(object id);
    }

    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly IConnectionProvider connectionProvider;

        public GenericRepository(IConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider;
        }

        public T GetById(object id)
        {
            using (var connection = connectionProvider.GetConnection())
            {
                connection.Open();

                return connection.Get<T>(id);
            }
        }

        public T GetByQuery(string query, object parameters)
        {
            using (var connection = connectionProvider.GetConnection())
            {
                connection.Open();

                return connection.QueryFirstOrDefault<T>(query, parameters);
            }
        }

        public ICollection<T> GetAll()
        {
            using (var connection = connectionProvider.GetConnection())
            {
                connection.Open();

                return connection.GetAll<T>().ToList();
            }
        }

        public ICollection<T> GetAllByQuery(string query, object parameters)
        {
            using (var connection = connectionProvider.GetConnection())
            {
                connection.Open();

                return connection.Query<T>(query, parameters).ToList();
            }
        }

        public long Add(T entity)
        {
            using (var connection = connectionProvider.GetConnection())
            {
                connection.Open();

                return connection.Insert(entity);
            }
        }

        public bool Update(T entity)
        {
            using (var connection = connectionProvider.GetConnection())
            {
                connection.Open();

                return connection.Update(entity);
            }
        }

        public bool Delete(T entity)
        {
            using (var connection = connectionProvider.GetConnection())
            {
                connection.Open();

                return connection.Delete(entity);
            }
        }

        public bool Delete(object id)
        {
            var entity = GetById(id);
            return Delete(entity);
        }
    }
}
