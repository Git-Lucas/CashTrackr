using CashTrackr.Domain.Transactions;

namespace CashTrackr.Application.Transactions.Commands.Events;

public interface ITransactionCreated
{
    event Func<Transaction, Task> OnTransactionCreated;
}
