using CashTrackr.Application.Transactions.Commands;
using CashTrackr.Application.Transactions.Events;

namespace CashTrackr.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddCommands()
            .AddEvents();

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
}
