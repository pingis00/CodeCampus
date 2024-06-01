using CodeCampus.Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using ResponseStatusCode = CodeCampus.Infrastructure.Responses.StatusCode;

namespace CodeCampus_WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly ILogger<CategoriesController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        try
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            if (result.Status == ResponseStatusCode.OK)
            {
                return Ok(result.ContentResult);
            }
            return StatusCode((int)result.Status, result.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving categories.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }
}