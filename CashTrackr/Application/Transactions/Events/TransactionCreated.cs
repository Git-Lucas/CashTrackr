using CashTrackr.Domain.Transactions;

namespace CashTrackr.Application.Transactions.Events;

public class TransactionCreated
{
    public void OnTransactionCreatedUpdateDailyBalance(Transaction transaction)
    {

    }
}
