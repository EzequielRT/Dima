using Dima.Core.Requests.Transactions;
using Dima.Core.Responses.Transactions;

namespace Dima.Core.Handlers;

public interface ITransactionHandler
{
    Task<CreateTransactionResponse> CreateAsync(CreateTransactionRequest request);
    Task<UpdateTransactionResponse> UpdateAsync(UpdateTransactionRequest request);
    Task<DeleteTransactionResponse> DeleteAsync(DeleteTransactionRequest request);
    Task<GetTransactionByIdResponse> GetByIdAsync(GetTransactionByIdRequest request);
    Task<GetTransactionsByPeriodResponse> GetByPeriodAsync(GetTransactionsByPeriodRequest request);
}
