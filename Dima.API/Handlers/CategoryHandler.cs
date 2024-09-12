using Dima.API.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses.Category;
using Microsoft.EntityFrameworkCore;

namespace Dima.API.Handlers;

public class CategoryHandler(AppDbContext _context) : ICategoryHandler
{
    public async Task<CreateCategoryResponse?> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description
            };

            await _context.AddAsync(category);
            await _context.SaveChangesAsync();

            return new CreateCategoryResponse(category, 201, "Categoria criada com sucesso");
        }
        catch
        {
            return new CreateCategoryResponse(null, 500, "[FP001] Não foi possível criar a categoria");
        }
    }

    public async Task<UpdateCategoryResponse?> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id &&
                                          x.UserId == request.UserId);

            if (category is null)
                return new UpdateCategoryResponse(null, 404, "[FP002] Categoria não encontrada");

            category.Title = request.Title;
            category.Description = request.Description;

            _context.Update(category);
            await _context.SaveChangesAsync();

            return new UpdateCategoryResponse(category, message: "Categoria atualizada com sucesso");
        }
        catch
        {
            return new UpdateCategoryResponse(null, 500, "[FP003] Não foi possível alterar a categoria");
        }
    }

    public async Task<DeleteCategoryResponse?> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id &&
                                          x.UserId == request.UserId);

            if (category is null)
                return new DeleteCategoryResponse(null, 404, "[FP004] Categoria não encontrada");

            _context.Remove(category);
            await _context.SaveChangesAsync();

            return new DeleteCategoryResponse(category, message: "Categoria excluída com sucesso");
        }
        catch
        {
            return new DeleteCategoryResponse(null, 500, "[FP005] Não foi possível excluir a categoria");
        }
    }

    public async Task<GetCategoryByIdResponse?> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id &&
                                          x.UserId == request.UserId);
            return category is null
                ? new GetCategoryByIdResponse(null, 404, "[FP006] Categoria não encontrada")
                : new GetCategoryByIdResponse(category);
        }
        catch
        {
            return new GetCategoryByIdResponse(null, 500, "[FP007] Não foi possível encontrar a categoria");
        }
    }

    public async Task<GetAllCategoriesResponse> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query = _context.Categories
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId)
            .OrderBy(x => x.Title)
            .AsQueryable();

            var count = await query.CountAsync();

            var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new GetAllCategoriesResponse(categories, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new GetAllCategoriesResponse(null, 500, "Não foi possível consultar as categorias");
        }
    }
}
