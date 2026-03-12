using ModularAuth.Domain.Common;
using ModularAuth.Application.DTOs.User;

namespace ModularAuth.Application.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponseResult>> RegisterAsync(RegisterRequestDto request);
    Task<Result<AuthResponseResult>> LoginAsync(LoginRequestDto request);
}
