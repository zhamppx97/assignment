using Microsoft.Extensions.DependencyInjection;

namespace api.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
        return services;
    }
}
