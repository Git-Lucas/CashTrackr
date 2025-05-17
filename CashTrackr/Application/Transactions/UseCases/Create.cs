using CashTrackr.Domain.Transactions;
using Type = CashTrackr.Domain.Transactions.Type;

namespace CashTrackr.Application.Transactions.UseCases;

public class Create(ITransactionRepository repository)
{
    private readonly ITransactionRepository _repository = repository;

    public async Task<CreateResponse> ExecuteAsync(CreateRequest request)
    {
        Transaction entity = request.ToEntity();

        await _repository.CreateAsync(entity);

        return new CreateResponse(CreatedId: entity.Id);
    }
}

public record CreateRequest(decimal Value, Type Type)
{
}

public record CreateResponse(int CreatedId)
{
}