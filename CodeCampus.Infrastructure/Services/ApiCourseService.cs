using Azure.Core;
using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Helpers;
using CodeCampus.Infrastructure.Interfaces.Services;
using CodeCampus.Infrastructure.Responses;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace CodeCampus.Infrastructure.Services;

public class ApiCourseService(ILogger<ApiCourseService> logger, HttpClientHelper httpClientHelper) : IApiCourseService
{
    private readonly ILogger<ApiCourseService> _logger = logger;
    private readonly HttpClientHelper _httpClientHelper = httpClientHelper;

    public async Task<ResponseResult> GetAllCoursesAsync()
    {
        var httpClient = _httpClientHelper.CreateHttpClientWithApiKey();
        try
        {
            var result = await httpClient.GetFromJsonAsync<IEnumerable<CourseDto>>("api/courses");
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
        var httpClient = _httpClientHelper.CreateHttpClientWithApiKey();
        try
        {
            var result = await httpClient.GetFromJsonAsync<CourseDto>($"api/courses/{id}");
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
