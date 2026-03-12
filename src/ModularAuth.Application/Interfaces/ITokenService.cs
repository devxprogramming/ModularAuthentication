using ModularAuth.Domain.Entities;

namespace ModularAuth.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(UserModel user);
    string GenerateRefreshToken();
}
