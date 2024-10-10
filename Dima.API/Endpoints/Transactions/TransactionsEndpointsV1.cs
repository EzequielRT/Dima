using Dima.API.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses.Transactions;

namespace Dima.API.Endpoints.Transactions;

public class TransactionsEndpointsV1
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
            .WithName("Transactions: Create")
            .WithSummary("Cria uma nova transação")
            .Produces<CreateTransactionResponse>()
            .WithOrder(1);

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, CreateTransactionRequest request)
        {
            var result = await handler.CreateAsync(request);

            return result.IsSuccess
                ? Results.Created($"/{result.Data?.Id}", result)
                : Results.BadRequest(result);
        }
    }

    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandleAsync)
            .WithName("Transactions: Update")
            .WithSummary("Atualiza uma transação")
            .Produces<UpdateTransactionResponse>()     
            .WithOrder(2);

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, [AsParameters] UpdateTransactionRequest request)
        {
            var result = await handler.UpdateAsync(request);

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.BadRequest(result);
        }
    }

    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("/{id}", HandleAsync)
            .WithName("Transactions: Delete")
            .WithSummary("Excluí uma transação")
            .Produces<DeleteTransactionResponse>()     
            .WithOrder(3);

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, [AsParameters] DeleteTransactionRequest request)
        {
            var result = await handler.DeleteAsync(request);

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.BadRequest(result);
        }
    }

    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
            .WithName("Transactions: Get By Id")
            .WithSummary("Retorna uma transação pelo Id")
            .Produces<GetTransactionByIdResponse>()     
            .WithOrder(4);

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, [AsParameters] GetTransactionByIdRequest request)
        {
            var result = await handler.GetByIdAsync(request);

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.BadRequest(result);
        }
    }

    public class GetTransactionsByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
            .WithName("Transactions: Get Transactions By Period")
            .WithSummary("Retorna as transações por período")
            .Produces<GetTransactionsByPeriodResponse>()  
            .WithOrder(5);

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, [AsParameters] GetTransactionsByPeriodRequest request)
        {
            var result = await handler.GetByPeriodAsync(request);

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.BadRequest(result);
        }
    }
}
