using System;
using ModularAuth.Application.DTOs.User;
using ModularAuth.Application.Interfaces;
using ModularAuth.Domain.Common;
using ModularAuth.Domain.Entities;

namespace ModularAuth.Application.AuthenticationServices;

public class AuthService : IAuthService
{
    // User repository to add user and save user to th DB
    private readonly IUserRepository _userRepository;
    // Password hasher service to hash the pasword, pass it to the user data before saving or creating user
    private readonly IPasswordHasher _passwordHasher;
    // Token service for generating Tokens for authentication. (JWT tokens)
    private readonly ITokenService _jwtTokenService;

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = tokenService;
    }
    
    // Login
    public async Task<Result<AuthResponseResult>> LoginAsync(LoginRequestDto request)
    {
        // 1. Get User
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            return Result<AuthResponseResult>.Failure("Invalid credentials.", "401");
        }

        // 2. Verify Password
        // Note: Your UserModel currently has PasswordHash as private set.
        // You might need to expose it via a property or method to verify here, 
        // or verify inside the entity. 
        // Ideally: user.VerifyPassword(request.Password, _passwordHasher)
        
        // Assuming we verify here for now:
        // We need access to user.PasswordHash. Currently it is private.
        // RECOMMENDATION: Change PasswordHash to public get, private set.
        bool isValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash); 
        
        if (!isValid)
        {
            return Result<AuthResponseResult>.Failure("Invalid credentials.", "401");
        }

        // 3. Generate Tokens
        var accessToken = _jwtTokenService.GenerateAccessToken(user);
        var refreshToken = _jwtTokenService.GenerateRefreshToken();

        // 4. Save Refresh Token
        user.SetRefreshTokens(refreshToken, DateTime.UtcNow.AddDays(7));
        // await _userRepository.UpdateAsync(user);

        return Result<AuthResponseResult>.Success(new AuthResponseResult
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            TokenExpiration = DateTime.UtcNow.AddHours(1),
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = $"{user.FirstName} {user.LastName}",
                CreatedAt = user.CreatedAt
            }
        });
    }


    // Register
    public async Task<Result<AuthResponseResult>> RegisterAsync(RegisterRequestDto request)
    {
        // Check if email exists already?
        if (await _userRepository.EmailExistsAsync(request.Email))
        {
            return Result<AuthResponseResult>.Failure("A user with this email exists already.", "409");
        }
        // Hash the passeword.
        var hashPassword = _passwordHasher.HashPassword(request.Password);

        // user instance for domain validation.
        var user = new UserModel(request.Username, request.Email, request.FirstName, request.LastName, hashPassword);

        await _userRepository.AddAsync(user);

        // Generate Token
        var accessToken = _jwtTokenService.GenerateAccessToken(user);
        var refreshToken = _jwtTokenService.GenerateRefreshToken();

        // add tokens set to user infor in DB
        user.SetRefreshTokens(refreshToken, DateTime.UtcNow.AddDays(1));
        await _userRepository.UpdateAsync(user);

        return Result<AuthResponseResult>.Success(new AuthResponseResult
        {
            User = new UserDto
            {
                Id = user.Id, 
                Email = user.Email, 
                FirstName = user.FirstName, 
                LastName = user.LastName, 
                FullName = $"{user.FirstName} {user.LastName}",
                CreatedAt = user.CreatedAt
            },
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            TokenExpiration = DateTime.UtcNow.AddDays(1)

        });
        
    }


}
