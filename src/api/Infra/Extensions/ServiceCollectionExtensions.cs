using api.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace api.Infra.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfraDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();

        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }

    public static IServiceCollection ConfigureInfraOptions(this IServiceCollection services)
    {
        return services;
    }
}
