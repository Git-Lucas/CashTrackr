using CashTrackr.Application.Transactions.Commands.Handlers;
using CashTrackr.Domain.Transactions;

namespace CashTrackr.Application.Transactions.Commands;

public static class Extensions
{
    public static Transaction ToEntity(this CreateRequest dto)
    {
        return new Transaction(
            date: DateOnly.FromDateTime(DateTime.UtcNow), 
            value: dto.Value, 
            type: dto.Type);
    }
}
