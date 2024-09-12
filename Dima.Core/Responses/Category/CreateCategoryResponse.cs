namespace Dima.Core.Responses.Category;

public class CreateCategoryResponse(
    Models.Category? category,
    int code = Configuration.DEFAULT_STATUS_CODE,
    string? message = null) : Response<Models.Category>(category, code, message)
{
}
