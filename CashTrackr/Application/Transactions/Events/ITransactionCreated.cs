using CashTrackr.Domain.Transactions;

namespace CashTrackr.Application.Transactions.Events;

public interface ITransactionCreated
{
    event Action<Transaction> OnTransactionCreated;
}
