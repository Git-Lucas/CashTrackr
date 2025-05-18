using CashTrackr.Domain.Transactions;

namespace CashTrackr.Tests.Domain.Transactions;
public class AmountTests
{
    [Fact]
    public void Amount_WhenPositiveDecimal_ValidObject()
    {
        // Arrange
        decimal value = 100.0m;

        // Act
        Amount amount = new(value);
        
        // Assert
        Assert.Equal(100.0m, amount.Value);
    }

    [Fact]
    public void Amount_WhenNegativeDecimal_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        decimal value = -100.0m;

        // Act
        ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Amount(value));

        // Assert
        Assert.Contains($"{nameof(Amount)} must be greater than 0.", exception.Message);
    }
}
