using System;
using System.Data;
using MySqlConnector;

namespace ModularAuth.Infrastructure.Data.DbConnection;

public class MySqlConnectionFactory : IDbConnectionFactory
{
    private string _connectionString;

    public MySqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    public IDbConnection CreateSyncConnection()
    {
        var connection = new MySqlConnection(_connectionString);
        connection.Open();
        return connection;
    }

    public async Task<IDbConnection> CreateAsyncConnection()
    {
        var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}
