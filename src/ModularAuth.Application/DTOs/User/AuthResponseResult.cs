using System;
using ModularAuth.Application.DTOs.Common;

namespace ModularAuth.Application.DTOs.User;

public class AuthResponseResult 
{
    public UserDto User { get; set; } = null!;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime TokenExpiration { get; set; }
}
