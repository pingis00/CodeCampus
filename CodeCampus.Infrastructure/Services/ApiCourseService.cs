using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Interfaces.Services;
using CodeCampus.Infrastructure.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace CodeCampus.Infrastructure.Services;

public class ApiCourseService(HttpClient httpClient, ILogger<ApiCourseService> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration) : IApiCourseService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<ApiCourseService> _logger = logger;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IConfiguration _configuration = configuration;

    public async Task<ResponseResult> GetAllCoursesAsync()
    {
        var apiKey = _configuration["ApiKey"];
        _httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
        try
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<CourseDto>>("api/courses");
            return new ResponseResult
            {
                Status = StatusCode.OK,
                ContentResult = result ?? [],
                Message = "Courses fetched successfully."
            };
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all courses.");
            return new ResponseResult
            {
                Status = StatusCode.INTERNAL_SERVER_ERROR,
                Message = "An error occurred while fetching all courses."
            };
        }
    }

    public async Task<ResponseResult> GetCourseByIdAsync(int id)
    {
        using var httpClient = _httpClientFactory.CreateClient();
        var apiKey = _configuration["ApiKey"];
        httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
        try
        {
            var result = await _httpClient.GetFromJsonAsync<CourseDto>($"api/courses/{id}");
            if (result == null)
            {
                _logger.LogWarning("Course with ID {Id} not found.", id);
                return new ResponseResult
                {
                    Status = StatusCode.NOT_FOUND,
                    Message = $"Course with ID {id} not found."
                };
            }
            return new ResponseResult
            {
                Status = StatusCode.OK,
                ContentResult = result,
                Message = "Course fetched successfully."
            };
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the course with ID {Id}.", id);
            return new ResponseResult
            {
                Status = StatusCode.INTERNAL_SERVER_ERROR,
                Message = "An error occurred while fetching the course."
            };
        }
    }
}
