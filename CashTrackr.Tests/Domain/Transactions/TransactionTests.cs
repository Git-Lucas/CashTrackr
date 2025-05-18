using CashTrackr.Domain.Transactions;
using Type = CashTrackr.Domain.Transactions.Type;

namespace CashTrackr.Tests.Domain.Transactions;
public class TransactionTests
{
    [Fact]
    public void Transaction_WhenValidInputs_CreatesTransaction()
    {
        // Arrange
        DateOnly date = new DateOnly(2024, 5, 1);
        decimal value = 150.0m;
        Type type = Type.Credit;

        // Act
        Transaction transaction = new(date, value, type);

        // Assert
        Assert.IsAssignableFrom<Transaction>(transaction);
    }

    [Fact]
    public void NormalizeTransactionValue_WhenCredit_ReturnsPositiveAmount()
    {
        // Arrange
        Transaction transaction = new(new DateOnly(2024, 5, 1), 200.0m, Type.Credit);

        // Act
        decimal result = transaction.NormalizeTransactionValue();

        // Assert
        Assert.Equal(200.0m, result);
    }

    [Fact]
    public void NormalizeTransactionValue_WhenDebit_ReturnsNegativeAmount()
    {
        // Arrange
        Transaction transaction = new(new DateOnly(2024, 5, 1), 200.0m, Type.Debit);

        // Act
        decimal result = transaction.NormalizeTransactionValue();

        // Assert
        Assert.Equal(-200.0m, result);
    }
}
