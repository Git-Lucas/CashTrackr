﻿using CashTrackr.Domain.Transactions;
using CashTrackr.Infrastructure.Data;
using CashTrackr.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CashTrackr.Infrastructure;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .AddContexts(configuration)
            .AddRepositories();

        return services;
    }

    public static async Task<IServiceProvider> AddInfrastructureAsync(this IServiceProvider services)
    {
        await services.MigrateAsync();

        return services;
    }

    private static IServiceCollection AddContexts(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<EfAdapterContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
        });

        services.AddStackExchangeRedisCache(options =>
        {
            string connectionString = configuration.GetConnectionString("RedisConnection")
                ?? throw new InvalidOperationException("Unable to read connection string from cache server.");

            options.Configuration = connectionString;
            options.InstanceName = "CashTrackr:";
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
