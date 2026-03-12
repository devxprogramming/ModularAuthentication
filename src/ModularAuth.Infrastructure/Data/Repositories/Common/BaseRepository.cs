using ModularAuth.Application.Interfaces.Base;
using ModularAuth.Domain.Common;
using ModularAuth.Infrastructure.Data.DbConnection;
using Dapper;

namespace ModularAuth.Application.Common;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly IDbConnectionFactory _dbConnection;
     protected readonly string _tableName; 
    public BaseRepository(IDbConnectionFactory dbConnectionFactory, string tablename)
    {
        _dbConnection = dbConnectionFactory;
        _tableName = tablename;
    }
    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        using var connection = await _dbConnection.CreateAsyncConnection();
        var sql = $"SELECT * FROM {_tableName} WHERE Id = @Id AND IsDeleted = 0";
        return await connection.QueryFirstOrDefaultAsync<TEntity>(sql, new { Id = id });
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        using var connection = await _dbConnection.CreateAsyncConnection();
        var sql = $"SELECT * FROM {_tableName} WHERE IsDeleted = 0";
        return await connection.QueryAsync<TEntity>(sql);
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        using var connection = await _dbConnection.CreateAsyncConnection();
        var sql = GenerateInsertSql();
        await connection.ExecuteAsync(sql, entity);
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        using var connection = await _dbConnection.CreateAsyncConnection();
        var sql = GenerateUpdateSql();
        await connection.ExecuteAsync(sql, entity);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        using var connection = await _dbConnection.CreateAsyncConnection();
        var sql = $"UPDATE {_tableName} SET IsDeleted = 1, DeletedAt = UTC_TIMESTAMP() WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }

    protected abstract string GenerateInsertSql();
    protected abstract string GenerateUpdateSql();
}
