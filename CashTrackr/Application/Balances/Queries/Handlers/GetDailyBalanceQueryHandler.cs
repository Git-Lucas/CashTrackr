using CashTrackr.Application.Transactions;
using Microsoft.Extensions.Caching.Distributed;

namespace CashTrackr.Application.Balances.Queries.Handlers;

public class GetDailyBalanceQueryHandler(IDistributedCache distributedCache)
{
    private readonly IDistributedCache _distributedCache = distributedCache;

    public async Task<decimal> HandleAsync(DateOnly date)
    {
        string dailyBalanceJson = await _distributedCache.GetStringAsync(TransactionKeys.GetDailyBalanceKey(date)) 
            ?? "0";

        return decimal.Parse(dailyBalanceJson);
    }
}
