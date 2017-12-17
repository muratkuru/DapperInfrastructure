using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DapperInfrastructure.Data
{
    public interface IConnectionProvider : IDisposable
    {
        IDbConnection GetConnection();
    }

    public class ConnectionProvider : IConnectionProvider
    {
        private readonly SqlConnection connection;
        private readonly string connectionString;

        public ConnectionProvider(string connectionStringOrName)
        {
            var connectionStrings = ConfigurationManager.ConnectionStrings[connectionStringOrName];

            this.connectionString = connectionStrings == null
                ? connectionStringOrName
                : connectionStrings.ConnectionString;

            connection = new SqlConnection(connectionString);
        }

        public IDbConnection GetConnection()
        {
            return connection == null || string.IsNullOrEmpty(connection.ConnectionString)
                ? new SqlConnection(connectionString)
                : connection;
        }

        #region Dispose
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                connection.Dispose();
            }

            disposed = true;
        }
        #endregion
    }
}
