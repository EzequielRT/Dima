using Dima.API.Data;
using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses.Category;
using Dima.Core.Responses.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Dima.API.Handlers;

public class TransactionHandler(AppDbContext _context) : ITransactionHandler
{
    public async Task<CreateTransactionResponse> CreateAsync(CreateTransactionRequest request)
    {
        try
        {
            var transaction = new Transaction
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.Now,
                Amount = request.Amount,
                PaidOrReceveidAt = request.PaidOrReceivedAt,
                Title = request.Title,
                Type = request.Type
            };

            await _context.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return new CreateTransactionResponse(transaction, 201, "Transação criada com sucesso");
        }
        catch
        {
            return new CreateTransactionResponse(null, 500, "[FP011] Não foi possível criar a transação");
        }
    }

    public async Task<UpdateTransactionResponse> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var transaction = await _context.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id &&
                                          x.UserId == request.UserId);

            if (transaction is null)
                return new UpdateTransactionResponse(null, 404, "[FP012] Transação não encontrada");
            
            transaction.CategoryId = request.CategoryId;
            transaction.Title = request.Title;
            transaction.Amount = request.Amount;
            transaction.Type = request.Type;
            transaction.PaidOrReceveidAt = request.PaidOrReceivedAt;

            _context.Update(transaction);
            await _context.SaveChangesAsync();

            return new UpdateTransactionResponse(transaction, message: "Transação atualizada com sucesso");
        }
        catch
        {
            return new UpdateTransactionResponse(null, 500, "[FP013] Não foi possível alterar a transação");
        }
    }

    public async Task<DeleteTransactionResponse> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await _context.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id &&
                                          x.UserId == request.UserId);

            if (transaction is null)
                return new DeleteTransactionResponse(null, 404, "[FP014] Transação não encontrada");

            _context.Remove(transaction);
            await _context.SaveChangesAsync();

            return new DeleteTransactionResponse(transaction, message: "Transação excluída com sucesso");
        }
        catch
        {
            return new DeleteTransactionResponse(null, 500, "[FP015] Não foi possível excluir a transação");
        }
    }

    public async Task<GetTransactionByIdResponse> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {
            var transaction = await _context.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id &&
                                          x.UserId == request.UserId);
            return transaction is null
                ? new GetTransactionByIdResponse(null, 404, "[FP016] Transação não encontrada")
                : new GetTransactionByIdResponse(transaction);
        }
        catch
        {
            return new GetTransactionByIdResponse(null, 500, "[FP017] Não foi possível encontrar a transação");
        }
    }

    public async Task<GetTransactionsByPeriodResponse> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
    {
        try
        {
            request.StartDate ??= DateTime.Now.GetFirstDay();
            request.EndDate ??= DateTime.Now.GetLastDay();
        }
        catch
        {
            return new GetTransactionsByPeriodResponse(null, 500, "[FP018] Não foi possível determinar a data de início ou término");
        }

        try
        {
            var query = _context.Transactions
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId &&
                            x.CreatedAt >= request.StartDate &&
                            x.CreatedAt <= request.EndDate)
                .OrderBy(x => x.Title)
                .AsQueryable();

            var count = await query.CountAsync();

            var transactions = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new GetTransactionsByPeriodResponse(transactions, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new GetTransactionsByPeriodResponse(null, 500, "[FP019] Não foi possível consultar as transações");
        }
    }
}
