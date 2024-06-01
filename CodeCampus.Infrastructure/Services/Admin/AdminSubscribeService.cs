using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Interfaces.Services.Admin;
using CodeCampus.Infrastructure.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace CodeCampus.Infrastructure.Services.Admin;

public class AdminSubscribeService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<AdminSubscribeService> logger) : IAdminSubscribeService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IConfiguration _configuration = configuration;
    private readonly ILogger<AdminSubscribeService> _logger = logger;

    public async Task<ResponseResult> GetAllAdminSubscribersAsync()
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient();
            var apiKey = _configuration["AdminApiKey"];
            httpClient.DefaultRequestHeaders.Add("X-Admin-Api-Key", apiKey);

            var response = await httpClient.GetAsync("https://localhost:7297/api/subscribe");

            if (response.IsSuccessStatusCode)
            {
                var subscribers = await response.Content.ReadFromJsonAsync<IEnumerable<SubscriberDto>>();
                return ResponseFactory.Ok(subscribers!);
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to fetch subscribers. Status code: {response.StatusCode}, Response: {responseBody}");

                return ResponseFactory.Error("Failed to fetch subscribers. Please try again later.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all admin subscribers");
            return ResponseFactory.Error("An error occurred while fetching subscribers.");
        }
    }

    public async Task<ResponseResult> GetAdminSubscriberByIdAsync(int id)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient();
            var apiKey = _configuration["AdminApiKey"];
            httpClient.DefaultRequestHeaders.Add("X-Admin-Api-Key", apiKey);

            var response = await httpClient.GetAsync($"https://localhost:7297/api/subscribe/{id}");

            if (response.IsSuccessStatusCode)
            {
                var subscriber = await response.Content.ReadFromJsonAsync<SubscriberDto>();
                return ResponseFactory.Ok(subscriber!);
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogError("Failed to fetch subscriber. Status code: {StatusCode}, Response: {ResponseBody}", response.StatusCode, responseBody);

                return ResponseFactory.Error("Failed to fetch subscriber. Please try again later.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching admin subscriber by id");
            return ResponseFactory.Error("An error occurred while fetching the subscriber.");
        }
    }

    public async Task<ResponseResult> DeleteAdminSubscriberAsync(int id)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient();
            var apiKey = _configuration["AdminApiKey"];
            httpClient.DefaultRequestHeaders.Add("X-Admin-Api-Key", apiKey);

            var response = await httpClient.DeleteAsync($"https://localhost:7297/api/subscribe/{id}");

            if (response.IsSuccessStatusCode)
            {
                return ResponseFactory.Ok("Subscriber deleted successfully.");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to delete subscriber. Status code: {response.StatusCode}, Response: {responseBody}");

                return ResponseFactory.Error("Failed to delete subscriber. Please try again later.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting admin subscriber");
            return ResponseFactory.Error("An error occurred while deleting the subscriber.");
        }
    }
}

