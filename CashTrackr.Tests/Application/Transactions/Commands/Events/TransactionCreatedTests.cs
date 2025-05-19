using CashTrackr.Application.Balances.Events;
using CashTrackr.Application.Transactions;
using CashTrackr.Domain.Transactions;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System.Text;
using Type = CashTrackr.Domain.Transactions.Type;

namespace CashTrackr.Tests.Application.Transactions.Commands.Events;
public class TransactionCreatedTests
{
    private static readonly DateOnly _date = new(2025, 5, 10);
    private static readonly string _key = TransactionKeys.GetDailyBalanceKey(_date);

    [Fact]
    public async Task OnTransactionCreatedUpdateDailyBalanceAsync_WhenCacheIsNull_SetsInitialBalance()
    {
        // Arrange
        Transaction transaction = new(_date, 100m, Type.Credit);

        Mock<IDistributedCache> mockCache = new();
        mockCache
            .Setup(c => c.GetAsync(_key, default))
            .ReturnsAsync((byte[]?)null);

        TransactionCreated handler = new(mockCache.Object);

        // Act
        await handler.OnTransactionCreatedUpdateDailyBalanceAsync(transaction);

        // Assert
        mockCache.Verify(c => c.SetAsync(
            _key,
            It.Is<byte[]>(b => Encoding.UTF8.GetString(b) == "100"),
            It.IsAny<DistributedCacheEntryOptions>(),
            default), Times.Once);
    }

    [Fact]
    public async Task OnTransactionCreatedUpdateDailyBalanceAsync_WhenCreditTransaction_SumValue()
    {
        // Arrange
        Transaction transaction = new(_date, 50m, Type.Credit);
        string existingBalance = "150";

        Mock<IDistributedCache> mockCache = new();
        mockCache
            .Setup(c => c.GetAsync(_key, default))
            .ReturnsAsync(Encoding.UTF8.GetBytes(existingBalance));

        TransactionCreated handler = new(mockCache.Object);

        // Act
        await handler.OnTransactionCreatedUpdateDailyBalanceAsync(transaction);

        // Assert
        mockCache.Verify(c => c.SetAsync(
            _key,
            It.Is<byte[]>(b => Encoding.UTF8.GetString(b) == "200"),
            It.IsAny<DistributedCacheEntryOptions>(),
            default), Times.Once);
    }

    [Fact]
    public async Task OnTransactionCreatedUpdateDailyBalanceAsync_WhenDebitTransaction_SubtractsValue()
    {
        // Arrange
        Transaction transaction = new(_date, 40m, Type.Debit);
        string existingBalance = "100";

        var mockCache = new Mock<IDistributedCache>();
        mockCache
            .Setup(c => c.GetAsync(_key, default))
            .ReturnsAsync(Encoding.UTF8.GetBytes(existingBalance));

        TransactionCreated handler = new(mockCache.Object);

        // Act
        await handler.OnTransactionCreatedUpdateDailyBalanceAsync(transaction);

        // Assert
        mockCache.Verify(c => c.SetAsync(
            _key,
            It.Is<byte[]>(b => Encoding.UTF8.GetString(b) == "60"),
            It.IsAny<DistributedCacheEntryOptions>(),
            default), Times.Once);
    }
}
