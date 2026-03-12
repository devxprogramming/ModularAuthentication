using System.Data;

namespace ModularAuth.Infrastructure.Data.DbConnection;

public interface IDbConnectionFactory
{
    public IDbConnection CreateSyncConnection();
    public Task<IDbConnection> CreateAsyncConnection();
}
