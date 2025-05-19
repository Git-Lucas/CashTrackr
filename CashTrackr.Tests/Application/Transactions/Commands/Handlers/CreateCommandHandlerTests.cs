using CashTrackr.Application.Balances.Events;
using CashTrackr.Application.Transactions.Commands.Handlers;
using CashTrackr.Domain.Transactions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using Type = CashTrackr.Domain.Transactions.Type;

namespace CashTrackr.Tests.Application.Transactions.Commands.Handlers;
public class CreateCommandHandlerTests
{
    [Fact]
    public async Task HandleAsync_CreatesTransactionAndFiresEvent_ReturnsCreatedId()
    {
        // Arrange
        Mock<ILogger<CreateCommandHandler>> loggerMock = new();

        Mock<ITransactionRepository> mockRepository = new();
        mockRepository
            .Setup(r => r.CreateAsync(It.IsAny<Transaction>()))
            .Callback<Transaction>(t => t.GetType()
                .GetProperty("Id")!
                .SetValue(t, 42))
            .Returns(Task.CompletedTask);

        Mock<IDistributedCache> mockCache = new();
        mockCache
            .Setup(c => c.GetAsync(It.IsAny<string>(), default))
            .ReturnsAsync((byte[]?)null);
        mockCache
            .Setup(c => c.SetAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), default))
            .Returns(Task.CompletedTask);

        TransactionCreated transactionCreated = new(mockCache.Object);

        CreateCommandHandler handler = new(loggerMock.Object, mockRepository.Object, transactionCreated);

        CreateRequest request = new(100m, Type.Credit);

        bool eventFired = false;
        handler.OnTransactionCreated += (Transaction tx) =>
        {
            eventFired = true;
            return Task.CompletedTask;
        };

        // Act
        CreateResponse result = await handler.HandleAsync(request);

        // Assert
        Assert.Equal(42, result.CreatedId);
        Assert.True(eventFired);
        mockRepository.Verify(r => r.CreateAsync(It.IsAny<Transaction>()), Times.Once);
    }
}
