using CashTrackr.Application.Transactions.Commands.Events;
using CashTrackr.Domain.Transactions;
using Type = CashTrackr.Domain.Transactions.Type;

namespace CashTrackr.Application.Transactions.Commands.Handlers;

public class CreateCommandHandler : ITransactionCreated
{
    private readonly ILogger<CreateCommandHandler> _logger;
    private readonly ITransactionRepository _repository;
    public event Func<Transaction, Task> OnTransactionCreated;
    
    public CreateCommandHandler(ILogger<CreateCommandHandler> logger, ITransactionRepository repository, TransactionCreated transactionCreated)
    {
        _logger = logger;
        _repository = repository;
        OnTransactionCreated += transactionCreated.OnTransactionCreatedUpdateDailyBalanceAsync;
    }

    public async Task<CreateResponse> HandleAsync(CreateRequest command)
    {
        Transaction entity = command.ToEntity();

        await _repository.CreateAsync(entity);
        _logger.LogInformation("Transaction created with ID: {Id}", entity.Id);

        await OnTransactionCreated.Invoke(entity);
        _logger.LogInformation("Transaction created event triggered for ID: {Id}", entity.Id);

        return new CreateResponse(CreatedId: entity.Id);
    }
}

public record CreateRequest(decimal Value, Type Type)
{
}

public record CreateResponse(int CreatedId)
{
}