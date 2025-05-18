using CashTrackr.Application.Balances.Queries.Handlers;
using CashTrackr.Application.Transactions;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System.Text;

namespace CashTrackr.Tests.Application.Balances.Queries.Handlers;
public class GetDailyBalanceQueryHandlerTests
{
    private static readonly DateOnly _date = new(2025, 5, 1);
    private static readonly string _key = TransactionKeys.GetDailyBalanceKey(_date);

    [Fact]
    public async Task HandleAsync_WhenCacheHasValidDecimal_ReturnsParsedValue()
    {
        // Arrange
        decimal expectedValue = 150.75m;
        byte[] cachedData = Encoding.UTF8.GetBytes(expectedValue.ToString());

        Mock<IDistributedCache> mockCache = new();
        mockCache.Setup(cache => cache.GetAsync(_key, default))
                 .ReturnsAsync(cachedData);

        GetDailyBalanceQueryHandler handler = new(mockCache.Object);

        // Act
        decimal result = await handler.HandleAsync(_date);

        // Assert
        Assert.Equal(expectedValue, result);
    }

    [Fact]
    public async Task HandleAsync_WhenCacheHasInvalidValue_ReturnsZero()
    {
        // Arrange
        byte[] invalidData = Encoding.UTF8.GetBytes("invalid_decimal");

        Mock<IDistributedCache> mockCache = new();
        mockCache.Setup(cache => cache.GetAsync(_key, default))
                 .ReturnsAsync(invalidData);

        GetDailyBalanceQueryHandler handler = new(mockCache.Object);

        // Act
        decimal result = await handler.HandleAsync(_date);

        // Assert
        Assert.Equal(0m, result);
    }

    [Fact]
    public async Task HandleAsync_WhenCacheReturnsNull_ReturnsZero()
    {
        // Arrange
        Mock<IDistributedCache> mockCache = new();
        mockCache.Setup(cache => cache.GetAsync(_key, default))
                 .ReturnsAsync((byte[]?)null);

        GetDailyBalanceQueryHandler handler = new(mockCache.Object);

        // Act
        decimal result = await handler.HandleAsync(_date);

        // Assert
        Assert.Equal(0m, result);
    }
}
