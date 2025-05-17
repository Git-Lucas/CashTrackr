using CashTrackr.Domain.Transactions;
using CashTrackr.Infrastructure.Data;
using CashTrackr.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashTrackr.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .AddContext(configuration)
            .AddRepositories();

        return services;
    }

    public static async Task<IServiceProvider> AddInfrastructureAsync(this IServiceProvider services)
    {
        await services.MigrateAsync();

        return services;
    }

    private static IServiceCollection AddContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<EfAdapterContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITransactionRepository, TransactionEF>();

        return services;
    }

    private static async Task<IServiceProvider> MigrateAsync(this IServiceProvider services)
    {
        using IServiceScope scope = services.CreateScope();
        EfAdapterContext context = scope.ServiceProvider.GetRequiredService<EfAdapterContext>();
        
        await context.Database.MigrateAsync();

        return services;
    }
}
