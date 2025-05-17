using CashTrackr.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace CashTrackr.Infrastructure.Data;

public class EfAdapterContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Transaction>()
            .OwnsOne(x => x.Amount)
            .Property(x => x.Value)
            .HasColumnName("Amount")
            .HasPrecision(18, 2);
    }
}
