using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Helpers;
using CodeCampus.Infrastructure.Interfaces.Services.Admin;
using CodeCampus.Infrastructure.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CodeCampus.Infrastructure.Services.Admin;

public class AdminCourseService(IConfiguration configuration, ILogger<AdminCourseService> logger, HttpClientHelper httpClientHelper) : IAdminCourseService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly ILogger<AdminCourseService> _logger = logger;
    private readonly HttpClientHelper _httpClientHelper = httpClientHelper;

    public async Task<ResponseResult> AddAdminCourseAsync(CourseRequestDto courseDto, IFormFile? courseImageFile)
    {
        try
        {
            var httpClient = _httpClientHelper.CreateHttpClientWithToken();
            var apiKey = _configuration["AdminApiKey"];
            httpClient.DefaultRequestHeaders.Add("X-Admin-Api-Key", apiKey);


            var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(courseDto.Title), "title");
            formData.Add(new StringContent(courseDto.Author), "author");
            formData.Add(new StringContent(courseDto.Price.ToString()), "price");

            if (courseDto.DiscountPrice.HasValue)
            {
                formData.Add(new StringContent(courseDto.DiscountPrice.ToString()!), "discountPrice");
            }

            formData.Add(new StringContent(courseDto.Hours.ToString()), "hours");
            formData.Add(new StringContent(courseDto.LikesInProcent.ToString()), "likesInProcent");
            formData.Add(new StringContent(courseDto.LikesInNumbers.ToString()), "likesInNumbers");
            formData.Add(new StringContent(courseDto.IsBestSeller.ToString()), "isBestSeller");
            formData.Add(new StringContent(courseDto.CategoryName), "categoryName");

            if (courseImageFile != null)
            {
                var fileContent = new StreamContent(courseImageFile.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(courseImageFile.ContentType);
                formData.Add(fileContent, "courseImageFile", courseImageFile.FileName);
            }

            var response = await httpClient.PostAsync("https://localhost:7043/api/courses/admin", formData);

            if (response.IsSuccessStatusCode)
            {
                return ResponseFactory.Ok("Course added successfully.");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return ResponseFactory.Unauthorized("Unauthorized. Please provide a valid API key.");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to add course. Status code: {response.StatusCode}, Response: {responseBody}");

                return ResponseFactory.Error("Failed to add course. Please try again later.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding course");
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> DeleteAdminCourseAsync(int id)
    {
        try
        {
            var httpClient = _httpClientHelper.CreateHttpClientWithToken();
            var apiKey = _configuration["AdminApiKey"];
            httpClient.DefaultRequestHeaders.Add("X-Admin-Api-Key", apiKey);

            var response = await httpClient.DeleteAsync($"https://localhost:7043/api/courses/admin/{id}");

            if (response.IsSuccessStatusCode)
            {
                return ResponseFactory.Ok("Course deleted successfully.");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return ResponseFactory.Unauthorized("Unauthorized. Please provide a valid API key.");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to delete course. Status code: {response.StatusCode}, Response: {responseBody}");

                return ResponseFactory.Error("Failed to delete course. Please try again later.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting course");
            return ResponseFactory.Error(ex.Message);
        }
    }

    public async Task<ResponseResult> GetAllAdminCoursesAsync()
    {
        try
        {
            var httpClient = _httpClientHelper.CreateHttpClientWithToken();
            var apiKey = _configuration["AdminApiKey"];
            httpClient.DefaultRequestHeaders.Add("X-Admin-Api-Key", apiKey);

            var response = await httpClient.GetAsync("https://localhost:7043/api/courses/admin");

            if (response.IsSuccessStatusCode)
            {
                var courses = await response.Content.ReadFromJsonAsync<IEnumerable<CourseGetRequestDto>>();

                return ResponseFactory.Ok(courses!);
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return ResponseFactory.Error("Failed to fetch courses. Please try again later.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all admin courses");
            return ResponseFactory.Error("An error occurred while fetching courses.");
        }
    }

    public async Task<ResponseResult> GetOneAdminCourseAsync(int id)
    {
        try
        {
            var httpClient = _httpClientHelper.CreateHttpClientWithToken();
            var apiKey = _configuration["AdminApiKey"];
            httpClient.DefaultRequestHeaders.Add("X-Admin-Api-Key", apiKey);

            var response = await httpClient.GetAsync($"https://localhost:7043/api/courses/admin/{id}");

            if (response.IsSuccessStatusCode)
            {
                var course = await response.Content.ReadFromJsonAsync<CourseGetRequestDto>();
                return ResponseFactory.Ok(course!);
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to fetch course. Status code: {response.StatusCode}, Response: {responseBody}");

                return ResponseFactory.Error("Failed to fetch course. Please try again later.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching admin course by id");
            return ResponseFactory.Error("An error occurred while fetching the course.");
        }
    }

    public async Task<ResponseResult> UpdateAdminCourseAsync(int id, CourseUpdateRequestDto courseDto, IFormFile? courseImageFile)
    {
        try
        {
            var httpClient = _httpClientHelper.CreateHttpClientWithToken();
            var apiKey = _configuration["AdminApiKey"];
            httpClient.DefaultRequestHeaders.Add("X-Admin-Api-Key", apiKey);

            var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(courseDto.Title), "title");
            formData.Add(new StringContent(courseDto.Author), "author");
            formData.Add(new StringContent(courseDto.Price.ToString()), "price");

            if (courseDto.DiscountPrice.HasValue)
            {
                formData.Add(new StringContent(courseDto.DiscountPrice.ToString()!), "discountPrice");
            }

            formData.Add(new StringContent(courseDto.Hours.ToString()), "hours");
            formData.Add(new StringContent(courseDto.LikesInProcent.ToString()), "likesInProcent");
            formData.Add(new StringContent(courseDto.LikesInNumbers.ToString()), "likesInNumbers");
            formData.Add(new StringContent(courseDto.IsBestSeller.ToString()), "isBestSeller");
            formData.Add(new StringContent(courseDto.CategoryName), "categoryName");

            if (courseImageFile != null)
            {
                var fileContent = new StreamContent(courseImageFile.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(courseImageFile.ContentType);
                formData.Add(fileContent, "courseImageFile", courseImageFile.FileName);
            }

            var response = await httpClient.PutAsync($"https://localhost:7043/api/courses/admin/{id}", formData);

            if (response.IsSuccessStatusCode)
            {
                return ResponseFactory.Ok("Course updated successfully.");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return ResponseFactory.Unauthorized("Unauthorized. Please provide a valid API key.");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to update course. Status code: {response.StatusCode}, Response: {responseBody}");

                return ResponseFactory.Error("Failed to update course. Please try again later.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating course");
            return ResponseFactory.Error(ex.Message);
        }
    }
}
