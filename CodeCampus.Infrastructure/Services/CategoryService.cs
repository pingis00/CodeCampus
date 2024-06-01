using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Interfaces.Repositories;
using CodeCampus.Infrastructure.Interfaces.Services;
using CodeCampus.Infrastructure.Responses;
using Microsoft.Extensions.Logging;
namespace CodeCampus.Infrastructure.Services;

public class CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly ILogger<CategoryService> _logger = logger;

    public async Task<ResponseResult> GetOrCreateCategoryAsync(string categoryName)
    {
        try
        {
            var existingCategoryResult = await _categoryRepository.GetOneAsync(c => c.CategoryName == categoryName);
            if (existingCategoryResult.Status == StatusCode.OK)
            {
                return existingCategoryResult;
            }

            var newCategory = new CategoryEntity { CategoryName = categoryName };
            var createResult = await _categoryRepository.CreateOneAsync(newCategory);
            return createResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetOrCreateCategoryAsync");
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> GetAllCategoriesAsync()
    {
        try
        {
            var result = await _categoryRepository.GetAllAsync();
            return new ResponseResult { Status = StatusCode.OK, ContentResult = result.ContentResult };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAllCategoriesAsync");
            return ResponseFactory.Error(ex.Message);
        }
    }
}
