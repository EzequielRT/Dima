using Dima.API.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses.Category;

namespace Dima.API.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
        .WithName("Categories: Delete")
        .WithSummary("Excluí uma categoria")
        .Produces<DeleteCategoryResponse>()     
        .WithOrder(3);

    private static async Task<IResult> HandleAsync(ICategoryHandler handler, [AsParameters] DeleteCategoryRequest request)
    {
        var result = await handler.DeleteAsync(request);

        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}