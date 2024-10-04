using Dima.API.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses.Category;

namespace Dima.API.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
        .WithName("Categories: Update")
        .WithSummary("Atualiza uma categoria")
        .Produces<UpdateCategoryResponse>()     
        .WithOrder(2);

    private static async Task<IResult> HandleAsync(ICategoryHandler handler, [AsParameters] UpdateCategoryRequest request)
    {
        var result = await handler.UpdateAsync(request);

        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}