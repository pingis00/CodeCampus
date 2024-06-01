using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Web.Helpers;
using CodeCampus.Web.ViewModels.Account;
using CodeCampus.Web.ViewModels.AvailableCourses;
using Microsoft.AspNetCore.Mvc;

namespace CodeCampus.Web.Controllers;

public class AvailableCoursesController(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<AvailableCoursesController> logger) : Controller
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ILogger<AvailableCoursesController> _logger = logger;

    [Route("/availablecourses")]
    public async Task<IActionResult> Index(string? category, string? searchQuery)
    {
        _logger.LogInformation("Index method called with category: {Category} and searchQuery: {SearchQuery}", category, searchQuery);
        try
        {
            using var http = _httpClientFactory.CreateClient();
            var apiKey = _configuration["ApiKey"];
            http.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

            _logger.LogInformation("Fetching categories from API");
            var categoriesResponse = await http.GetAsync("https://localhost:7297/api/categories");

            if (!categoriesResponse.IsSuccessStatusCode)
            {
                TempData["Message"] = "Error fetching categories from API.";
                TempData["MessageType"] = "error";
                _logger.LogError("Error fetching categories from API. Status code: {StatusCode}", categoriesResponse.StatusCode);
                return View("Error");
            }

            var categoryDtos = await categoriesResponse.Content.ReadFromJsonAsync<IEnumerable<CategoryDto>>();
            if (categoryDtos == null)
            {
                TempData["Message"] = "Error fetching categories from API.";
                TempData["MessageType"] = "error";
                _logger.LogError("Error fetching categories from API. No categories returned.");
                return View("Error");
            }
            var categoryViewModels = categoryDtos.Select(AdminMappingFactory.MapToViewModel).ToList();
            _logger.LogInformation("Categories fetched: {@Categories}", categoryViewModels);

            var encodedCategory = Uri.EscapeDataString(category ?? "all");
            var encodedSearchQuery = Uri.EscapeDataString(searchQuery ?? string.Empty);
            var requestUrl = $"https://localhost:7297/api/courses?category={encodedCategory}&searchQuery={encodedSearchQuery}";
            var response = await http.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Error fetching courses from API.";
                TempData["MessageType"] = "error";
                _logger.LogError("Error fetching courses from API. Status code: {StatusCode}", response.StatusCode);
                return View("Error");
            }

            var courseDtos = await response.Content.ReadFromJsonAsync<IEnumerable<CourseDto>>();
            if (courseDtos == null)
            {
                TempData["Message"] = "Error fetching courses from API.";
                TempData["MessageType"] = "error";
                _logger.LogError("Error fetching courses from API. No courses returned.");
                return View("Error");
            }

            var courseViewModels = courseDtos.Select(AdminMappingFactory.MapToViewModel).ToList();
            var courseComponents = courseViewModels.Select(AdminMappingFactory.MapToCourseComponent).ToList();
            _logger.LogInformation("Courses fetched: {@Courses}", courseViewModels);


            var viewModel = new AvailableCoursesViewModel
            {
                CourseSearch = new CourseSearchViewModel
                {
                    Categories = categoryViewModels,
                    SelectedCategory = category ?? "all",
                    SearchQuery = searchQuery!
                },
                Courses = new SavedCoursesViewModel
                {
                    Courses = courseComponents
                }
            };
            ViewData["ShowDeleteButton"] = false;
            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while fetching courses.");
            TempData["Message"] = "An unexpected error occurred. Please try again later.";
            TempData["MessageType"] = "error";
            return View("Error");
        }
    }
}