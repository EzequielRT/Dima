using Dima.API.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses.Category;
using System.Security.Claims;

namespace Dima.API.Endpoints.Categories;

public class CategoriesEndpointsV1
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Cria uma nova categoria")
            .Produces<CreateCategoryResponse>()        
            .WithOrder(1);

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler, CreateCategoryRequest request)
        {
            request.SetUserId(user);
            var result = await handler.CreateAsync(request);

            return result.IsSuccess
                ? Results.Created($"/{result.Data?.Id}", result)
                : Results.BadRequest(result);
        }
    }

    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza uma categoria")
            .Produces<UpdateCategoryResponse>()     
            .WithOrder(2);

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler, [AsParameters] UpdateCategoryRequest request)
        {
            request.SetUserId(user);
            var result = await handler.UpdateAsync(request);

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.BadRequest(result);
        }
    }

    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("/{id}", HandleAsync)
            .WithName("Categories: Delete")
            .WithSummary("Excluí uma categoria")
            .Produces<DeleteCategoryResponse>()     
            .WithOrder(3);

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler, [AsParameters] DeleteCategoryRequest request)
        {
            request.SetUserId(user);
            var result = await handler.DeleteAsync(request);

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.BadRequest(result);
        }
    }

    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
            .WithName("Categories: Get By Id")
            .WithSummary("Retorna uma categoria pelo Id")
            .Produces<GetCategoryByIdResponse>()     
            .WithOrder(4);

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler, [AsParameters] GetCategoryByIdRequest request)
        {
            request.SetUserId(user);
            var result = await handler.GetByIdAsync(request);

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.BadRequest(result);
        }
    }

    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
            .WithName("Categories: Get All")
            .WithSummary("Retorna todas as categorias")
            .Produces<GetAllCategoriesResponse>()  
            .WithOrder(5);

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler, [AsParameters] GetAllCategoriesRequest request)
        {
            request.SetUserId(user);
            var result = await handler.GetAllAsync(request);

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.BadRequest(result);
        }
    }
}
