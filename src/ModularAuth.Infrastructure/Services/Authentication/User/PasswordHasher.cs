using ModularAuth.Application.Interfaces;

namespace ModularAuth.Infrastructure.Services.Authentication.User;

public class PasswordHasher : IPasswordHasher
{
    // HashPassword()
    public string HashPassword(string password)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        return hashedPassword;
    }
    

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
