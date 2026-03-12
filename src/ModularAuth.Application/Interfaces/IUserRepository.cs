using System;
using ModularAuth.Application.Interfaces.Base;
using ModularAuth.Domain.Common;
using ModularAuth.Domain.Entities;

namespace ModularAuth.Application.Interfaces;

public interface IUserRepository : IBaseRepository<UserModel>
{
    // Gets a user by their email address.
    Task<UserModel?> GetByEmailAsync(string email);
    
    // Gets all active users.
    Task<IEnumerable<UserModel>> GetActiveUserModelsAsync();
    
    /// Gets UserModels by role.
    // Task<IEnumerable<UserModel>> GetUsersByRoleAsync(string role);
    
    // Checks if an email is already registered.
    Task<bool> EmailExistsAsync(string email);
}
