namespace CashTrackr.Application.Transactions;

public static class TransactionKeys
{
    public static string GetDailyBalanceKey(DateOnly date) => $"daily_balance:{date:yyyy-MM-dd}";
}
