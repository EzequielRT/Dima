using Dima.API.Common.Api;
using Dima.API.Endpoints.Categories;
using Dima.API.Endpoints.Transactions;

namespace Dima.API.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("v1/categories")
            .WithTags("Categories")
            //.RequireAuthorization()
            .MapEndpoint<CategoriesEndpointsV1.CreateCategoryEndpoint>()
            .MapEndpoint<CategoriesEndpointsV1.UpdateCategoryEndpoint>()
            .MapEndpoint<CategoriesEndpointsV1.DeleteCategoryEndpoint>()
            .MapEndpoint<CategoriesEndpointsV1.GetCategoryByIdEndpoint>()
            .MapEndpoint<CategoriesEndpointsV1.GetAllCategoriesEndpoint>();

        endpoints.MapGroup("v1/transactions")
            .WithTags("Transactions")
            //.RequireAuthorization()
            .MapEndpoint<TransactionsEndpointsV1.CreateTransactionEndpoint>()
            .MapEndpoint<TransactionsEndpointsV1.UpdateTransactionEndpoint>()
            .MapEndpoint<TransactionsEndpointsV1.DeleteTransactionEndpoint>()
            .MapEndpoint<TransactionsEndpointsV1.GetTransactionByIdEndpoint>()
            .MapEndpoint<TransactionsEndpointsV1.GetTransactionsByPeriodEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
