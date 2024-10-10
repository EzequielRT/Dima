namespace Dima.Core.Responses.Transactions;

public class GetTransactionsByPeriodResponse : PagedResponse<List<Models.Transaction>>
{
    public GetTransactionsByPeriodResponse(
        List<Models.Transaction>? models,
        int totalCount,
        int currentPage = Configuration.DEFAULT_PAGE_NUMBER,
        int pageSize = Configuration.DEFAULT_PAGE_SIZE) : base(models, totalCount, currentPage, pageSize) { }

    public GetTransactionsByPeriodResponse(
        List<Models.Transaction>? models,
        int code = Configuration.DEFAULT_STATUS_CODE,
        string? message = null) : base(models, code, message) { }
}