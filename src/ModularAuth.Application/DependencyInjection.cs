using Microsoft.Extensions.DependencyInjection;
using ModularAuth.Application.Interfaces;
using ModularAuth.Application.AuthenticationServices;


namespace ModularAuth.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register Application Services
        services.AddScoped<IAuthService, AuthService>();
        
        // You can add AutoMapper, Validators, or MediatR here later if needed.

        return services;
    }
}
