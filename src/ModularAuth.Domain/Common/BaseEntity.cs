using System;

namespace ModularAuth.Domain.Common;

public class BaseEntity
{
    public Guid Id {get; protected set;}
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }
    public bool IsDeleted { get; protected set; }
    public DateTime? DeletedAt { get; protected set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }
    
    protected BaseEntity(Guid id)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }
}
