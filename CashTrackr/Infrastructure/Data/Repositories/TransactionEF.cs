using CashTrackr.Domain.Transactions;

namespace CashTrackr.Infrastructure.Data.Repositories;

public class TransactionEF(EfAdapterContext context) : ITransactionRepository
{
    private readonly EfAdapterContext _context = context;

    public async Task CreateAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }
}
