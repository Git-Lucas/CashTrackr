using CashTrackr.Domain.Transactions;
using System.Diagnostics.CodeAnalysis;

namespace CashTrackr.Infrastructure.Data.Repositories;

[ExcludeFromCodeCoverage]
public class TransactionEF(EfAdapterContext context) : ITransactionRepository
{
    private readonly EfAdapterContext _context = context;

    public async Task CreateAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }
}
