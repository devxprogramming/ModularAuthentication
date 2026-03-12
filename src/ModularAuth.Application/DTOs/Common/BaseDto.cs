using System;

namespace ModularAuth.Application.DTOs.Common;

public class BaseDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
}
