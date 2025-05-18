namespace CashTrackr.Domain.Transactions;

public interface ITransactionRepository
{
    Task CreateAsync(Transaction transaction);
}
