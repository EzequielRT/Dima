using Dima.Core.Requests.Categories;
using Dima.Core.Responses.Category;

namespace Dima.Core.Handlers;

public interface ICategoryHandler
{
    Task<CreateCategoryResponse> CreateAsync(CreateCategoryRequest request);
    Task<UpdateCategoryResponse> UpdateAsync(UpdateCategoryRequest request);
    Task<DeleteCategoryResponse> DeleteAsync(DeleteCategoryRequest request);
    Task<GetCategoryByIdResponse> GetByIdAsync(GetCategoryByIdRequest request);
    Task<GetAllCategoriesResponse> GetAllAsync(GetAllCategoriesRequest request);
}
