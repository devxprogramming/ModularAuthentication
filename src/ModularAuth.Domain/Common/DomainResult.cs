
namespace ModularAuth.Domain.Common;

public record class DomainResult<TEntity, TResult>  
    where TEntity : BaseEntity
    where TResult : Result<TEntity>
{
    public string Username {get; init;} = string.Empty;
    public string Email {get;} = string.Empty;
    public string Fristname {get;}
    public string Lastname {get;}


    private DomainResult(string username, string email, string firstname, string lastname)
    {
        Username = username;
        Email = email;
        Fristname = firstname;
        Lastname = lastname;
    }

    
}
