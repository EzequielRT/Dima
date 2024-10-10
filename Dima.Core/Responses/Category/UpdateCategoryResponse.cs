namespace Dima.Core.Responses.Category;

public class UpdateCategoryResponse(
    Models.Category? model,
    int code = Configuration.DEFAULT_STATUS_CODE,
    string? message = null) : Response<Models.Category>(model, code, message)
{
}