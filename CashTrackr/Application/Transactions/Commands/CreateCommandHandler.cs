using CashTrackr.Application.Transactions.Events;
using CashTrackr.Domain.Transactions;
using Type = CashTrackr.Domain.Transactions.Type;

namespace CashTrackr.Application.Transactions.Commands;

public class CreateCommandHandler : ITransactionCreated
{
    private readonly ITransactionRepository _repository;
    public event Action<Transaction> OnTransactionCreated;
    
    public CreateCommandHandler(ITransactionRepository repository, TransactionCreated transactionCreated)
    {
        _repository = repository;
        OnTransactionCreated += transactionCreated.OnTransactionCreatedUpdateDailyBalance;
    }

    public async Task<CreateResponse> HandleAsync(CreateRequest command)
    {
        Transaction entity = command.ToEntity();

        await _repository.CreateAsync(entity);

        OnTransactionCreated.Invoke(entity);

        return new CreateResponse(CreatedId: entity.Id);
    }
}

public record CreateRequest(decimal Value, Type Type)
{
}

public record CreateResponse(int CreatedId)
{
}