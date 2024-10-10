namespace Dima.Core.Responses.Transactions;

public class DeleteTransactionResponse(
    Models.Transaction? model,
    int code = Configuration.DEFAULT_STATUS_CODE,
    string? message = null) : Response<Models.Transaction>(model, code, message)
{
}

