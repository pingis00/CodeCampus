using CodeCampus.Infrastructure.Contexts;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Interfaces.Repositories;
using CodeCampus.Infrastructure.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeCampus.Infrastructure.Repositories;

public class CategoryRepository(DataContext context, ILogger<CategoryRepository> logger) : Base<CategoryEntity>(context, logger), ICategoryRepository
{
    public override async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            var result = await _context.Categories.OrderBy(c => c.CategoryName).ToListAsync();
            return ResponseFactory.Ok(CategoryFactory.Create(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving all categories ordered by name");
            return ResponseFactory.Error(ex.Message);
        }
    }
}
