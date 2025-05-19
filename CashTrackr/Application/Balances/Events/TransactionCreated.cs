using CashTrackr.Application.Transactions;
using CashTrackr.Domain.Transactions;
using Microsoft.Extensions.Caching.Distributed;

namespace CashTrackr.Application.Balances.Events;

public class TransactionCreated(IDistributedCache distributedCache)
{
    private readonly IDistributedCache _distributedCache = distributedCache;

    public async Task OnTransactionCreatedUpdateDailyBalanceAsync(Transaction transaction)
    {
        string dailyBalanceJson = await _distributedCache.GetStringAsync(TransactionKeys.GetDailyBalanceKey(transaction.Date))
            ?? "0";
        decimal dailyBalance = decimal.Parse(dailyBalanceJson);

        dailyBalance += transaction.NormalizeTransactionValue();

        await _distributedCache.SetStringAsync(
            key: TransactionKeys.GetDailyBalanceKey(transaction.Date),
            value: dailyBalance.ToString(),
            options: new DistributedCacheEntryOptions());
    }
}
