using CashTrackr.Application.Transactions.UseCases;
using CashTrackr.Domain.Transactions;

namespace CashTrackr.Application.Transactions;

public static class Extensions
{
    public static Transaction ToEntity(this CreateRequest dto)
    {
        return new Transaction(
            date: DateTime.UtcNow, 
            value: dto.Value, 
            type: dto.Type);
    }
}
