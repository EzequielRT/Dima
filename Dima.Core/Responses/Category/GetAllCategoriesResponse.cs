namespace Dima.Core.Responses.Category;

public class GetAllCategoriesResponse : PagedResponse<List<Models.Category>>
{
    public GetAllCategoriesResponse(
        List<Models.Category>? categories,
        int totalCount,
        int currentPage = Configuration.DEFAULT_PAGE_NUMBER,
        int pageSize = Configuration.DEFAULT_PAGE_SIZE) : base(categories, totalCount, currentPage, pageSize) { }

    public GetAllCategoriesResponse(
        List<Models.Category>? categories,
        int code = Configuration.DEFAULT_STATUS_CODE,
        string? message = null) : base(categories, code, message) { }
}