namespace Dima.Core.Responses.Transactions;

public class GetTransactionByIdResponse(
    Models.Transaction? model,
    int code = Configuration.DEFAULT_STATUS_CODE,
    string? message = null) : Response<Models.Transaction>(model, code, message)
{
}
