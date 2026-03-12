using System;
using System.Runtime.CompilerServices;
using ModularAuth.Domain.Common;
using ModularAuth.Domain.Enums;

namespace ModularAuth.Domain.Entities;

public class UserModel : BaseEntity
{
    public string Username {get; private set;} = string.Empty;
    public string PasswordHash {get; internal set;} = string.Empty;
    public string FirstName {get; private set;} = string.Empty;
    public string LastName {get; private set;} = string.Empty;
    public string Email {get; private set;} = string.Empty;
    public bool IsActive {get; set;}

    public string RefreshToken {get; private set;} = string.Empty;
    public DateTime? RefreshTokenExpiryTime { get; private set; }

    // User Role or List of roles
    List<Result<string>> UserRoles = new List<Result<string>>();

    // List of User Permisson
    List<UserPermissions> UserPermissions = new();


    // For Dapper ORM
    private UserModel()
    {
        
    }

    public UserModel(string username, string email, string firstname, string lastname, string passwordhash)
    {
        ValidateEmail(email);
        ValidateFirstnameAndLastName(firstname, lastname);

        Username = username.Trim();
        PasswordHash = passwordhash;
        FirstName = firstname.Trim();
        LastName = lastname.Trim();
        Email = email.Trim();
        IsActive = true;

        UpdateTimeStamp();
    }

    // Behaviours

    // Valicate Email to make sure we have a valid email address
    private static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));
        
        if (!email.Contains('@') || !email.Contains('.'))
            throw new ArgumentException("Invalid email format.", nameof(email));
    }

    // Validate firstname and lastname to make sure we do not store empty values
    private static void ValidateFirstnameAndLastName(string firstname, string lastname)
    {
          if (string.IsNullOrWhiteSpace(firstname))
            throw new ArgumentException("First name cannot be empty.", nameof(firstname));
        
        if (string.IsNullOrWhiteSpace(lastname))
            throw new ArgumentException("Last name cannot be empty.", nameof(lastname));
    }

    public void SetRefreshTokens(string token, DateTime expiryTime)
    {
        RefreshToken = token;
        RefreshTokenExpiryTime = expiryTime;
        UpdateTimeStamp();
    }

    // Check if user has valid RefreshToken
    public bool HasValidRefreshToken(string token)
    {
        return RefreshToken == token 
            && RefreshTokenExpiryTime.HasValue 
            && RefreshTokenExpiryTime.Value > DateTime.UtcNow;
    }

    // Clear RefreshToken for Logout
    public void ClearRefreshToken()
    {
        RefreshToken = string.Empty;
        RefreshTokenExpiryTime = null;
        UpdateTimeStamp();
    }

    // De-activate user profile
    public void Deactivate()
    {
        this.IsActive = false;
        UpdateTimeStamp();
    }
    // Reactivate user
    public void Activate()
    {
        IsActive = true;
        UpdateTimeStamp();
    }

    // Update Timestamp function.
    public void UpdateTimeStamp()
    {
        UpdatedAt = DateTime.UtcNow;
        Console.WriteLine(UpdatedAt);
    }

    



}
