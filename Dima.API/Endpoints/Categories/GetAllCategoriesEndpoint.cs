using Dima.API.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses.Category;

namespace Dima.API.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
        .WithName("Categories: Get All")
        .WithSummary("Retorna todas as categorias")
        .Produces<GetAllCategoriesResponse>()  
        .WithOrder(5);

    private static async Task<IResult> HandleAsync(ICategoryHandler handler, [AsParameters] GetAllCategoriesRequest request)
    {
        var result = await handler.GetAllAsync(request);

        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}