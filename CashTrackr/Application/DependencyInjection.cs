using CashTrackr.Application.Balances.Events;
using CashTrackr.Application.Balances.Queries.Handlers;
using CashTrackr.Application.Transactions.Commands.Handlers;
using System.Diagnostics.CodeAnalysis;

namespace CashTrackr.Application;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddCommands()
            .AddEvents()
            .AddQueries();

        return services;
    }

    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.AddScoped<CreateCommandHandler>();

        return services;
    }

    private static IServiceCollection AddEvents(this IServiceCollection services)
    {
        services.AddScoped<TransactionCreated>();
        
        return services;
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddScoped<GetDailyBalanceQueryHandler>();

        return services;
    }
}
