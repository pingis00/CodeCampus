using CodeCampus.Infrastructure.Responses;

namespace CodeCampus.Infrastructure.Interfaces.Services;

public interface ICategoryService
{
    Task<ResponseResult> GetOrCreateCategoryAsync(string categoryName);
    Task<ResponseResult> GetAllCategoriesAsync();
}
