using System;
using ModularAuth.Application.DTOs.Common;

namespace ModularAuth.Application.DTOs.User;

public class UserDto : BaseDto
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}
