using Dima.API.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses.Category;

namespace Dima.API.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
        .WithName("Categories: Get By Id")
        .WithSummary("Retorna uma categoria pelo Id")
        .Produces<GetCategoryByIdResponse>()     
        .WithOrder(4);

    private static async Task<IResult> HandleAsync(ICategoryHandler handler, [AsParameters] GetCategoryByIdRequest request)
    {
        var result = await handler.GetByIdAsync(request);

        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}