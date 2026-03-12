using Dapper;
using ModularAuth.Application.Common;
using ModularAuth.Application.Interfaces;
using ModularAuth.Domain.Entities;
using ModularAuth.Domain.Enums;
using ModularAuth.Infrastructure.Data.DbConnection;

namespace ModularAuth.Infrastructure.Data.Repositories;

public class UserRepository : BaseRepository<UserModel>, IUserRepository
{
    private readonly IDbConnectionFactory _dbConnection;
    public UserRepository(IDbConnectionFactory dbConnectionFactory) :
    base(dbConnectionFactory, "Users")
    {
        _dbConnection = dbConnectionFactory;
        
    }
    public async Task<UserModel?> GetByEmailAsync(string email)
    {
        const string sql = @"
            SELECT 
                Id, Username, Email, FirstName, LastName, PasswordHash, 
                RefreshToken, RefreshTokenExpiryTime, 
                CreatedAt, UpdatedAt, IsDeleted, IsActive
            FROM Users 
            WHERE Email = @Email 
              AND IsActive = 1
            LIMIT 1;";
        
        using var connection = await _dbConnection.CreateAsyncConnection();
        
        return await connection.QuerySingleOrDefaultAsync<UserModel>(
            sql, 
            new { Email = email });
    }

    public async Task<IEnumerable<UserModel>> GetActiveUserModelsAsync()
    {
        const string sql = @"
            SELECT 
                Id, Username, Email, FirstName, LastName, PasswordHash, 
                RefreshToken, RefreshTokenExpiryTime, 
                CreatedAt, UpdatedAt, IsDeleted, IsActive
            FROM Users 
            WHERE IsActive = 1 
              AND IsDeleted = 0
            ORDER BY FirstName, LastName";
        
        using var connection = await _dbConnection.CreateAsyncConnection();
        
        return await connection.QueryAsync<UserModel>(sql);
    }
    
    // public async Task<IEnumerable<UserModel>> GetUsersByRoleAsync(string role)
    // {
    //     if (!Enum.TryParse<Roles>(role, true, out var userRole))
    //         return new List<UserModel>();
        
    //     const string sql = @"
    //         SELECT * FROM Users 
    //         WHERE Role = @Role 
    //           AND IsActive = 1 
    //           AND IsDeleted = 0
    //         ORDER BY FirstName, LastName";
        
    //     using var connection = await _dbConnection.CreateAsyncConnection();
        
    //     return await connection.QueryAsync<UserModel>(
    //         sql, 
    //         new { Role = (int)userRole });
    // }
    
    public async Task<bool> EmailExistsAsync(string email)
    {
        const string sql = @"
            SELECT COUNT(1) FROM Users 
            WHERE Email = @Email 
              AND IsActive = 1";
        
        using var connection = await _dbConnection.CreateAsyncConnection();
        
        var count = await connection.ExecuteScalarAsync<int>(
            sql, 
            new { Email = email });
        
        return count > 0;
    }

    public override async Task AddAsync(UserModel entity)
    {
        
        var sql = @"
            INSERT INTO Users (
                Id, Username, Email, FirstName, LastName, PasswordHash, 
                IsActive, CreatedAt, UpdatedAt
            ) VALUES (
                @Id, @Username, @Email, @FirstName, @LastName, @PasswordHash, 
                @IsActive, @CreatedAt, @UpdatedAt
            );";
            
        using var connection = await _dbConnection.CreateAsyncConnection();
            
        await connection.ExecuteAsync(sql, new
        {
            entity.Id,
            entity.Username,
            entity.Email,
            entity.FirstName,
            entity.LastName,
            entity.PasswordHash,
            entity.IsActive,
            entity.CreatedAt,
            entity.UpdatedAt,
        });
    }
    
    public override async Task UpdateAsync(UserModel entity)
    {
        using var connection = await _dbConnection.CreateAsyncConnection();
        var sql = @"
            UPDATE Users SET
                FirstName = @FirstName,
                LastName = @LastName,
                PasswordHash = @PasswordHash,
                RefreshToken = @RefreshToken,
                RefreshTokenExpiryTime = @RefreshTokenExpiryTime,
                UpdatedAt = UTC_TIMESTAMP()
            WHERE Id = @Id;";
        await connection.ExecuteAsync(sql, new
        {
            entity.FirstName,
            entity.LastName,
            entity.PasswordHash,
            entity.RefreshToken,
            entity.RefreshTokenExpiryTime,
            entity.Id,
        });
    }
    
    protected override string GenerateInsertSql()
    {
        // Not used since we override AddAsync
        return string.Empty;
    }
    
    protected override string GenerateUpdateSql()
    {
        // Not used since we override UpdateAsync
        return string.Empty;
    }
}
