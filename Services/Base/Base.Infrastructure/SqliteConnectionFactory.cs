using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Base.Infrastructure
{
    public class SqliteConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public SqliteConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open) _connection.Dispose();
        }

        public IDbConnection GetOpenConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = new SqliteConnection(_connectionString);
                _connection.Open();
            }

            return _connection;
        }
    }
}
