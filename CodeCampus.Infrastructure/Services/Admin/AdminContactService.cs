using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Interfaces.Services.Admin;
using CodeCampus.Infrastructure.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace CodeCampus.Infrastructure.Services.Admin;

public class AdminContactService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<AdminContactService> logger) : IAdminContactService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IConfiguration _configuration = configuration;
    private readonly ILogger<AdminContactService> _logger = logger;

    public async Task<ResponseResult> GetAllAdminContactsAsync()
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient();
            var apiKey = _configuration["AdminApiKey"];
            httpClient.DefaultRequestHeaders.Add("X-Admin-Api-Key", apiKey);

            var response = await httpClient.GetAsync("https://localhost:7297/api/contact");

            if (response.IsSuccessStatusCode)
            {
                var contacts = await response.Content.ReadFromJsonAsync<IEnumerable<ContactRequestDto>>();
                return ResponseFactory.Ok(contacts!);
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to fetch contacts. Status code: {response.StatusCode}, Response: {responseBody}");

                return ResponseFactory.Error("Failed to fetch contacts. Please try again later.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all admin contacts");
            return ResponseFactory.Error("An error occurred while fetching contacts.");
        }
    }

    public async Task<ResponseResult> GetAdminContactByIdAsync(int id)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient();
            var apiKey = _configuration["AdminApiKey"];
            httpClient.DefaultRequestHeaders.Add("X-Admin-Api-Key", apiKey);

            var response = await httpClient.GetAsync($"https://localhost:7297/api/contact/{id}");

            if (response.IsSuccessStatusCode)
            {
                var contact = await response.Content.ReadFromJsonAsync<ContactRequestDto>();
                return ResponseFactory.Ok(contact!);
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to fetch contact. Status code: {response.StatusCode}, Response: {responseBody}");

                return ResponseFactory.Error("Failed to fetch contact. Please try again later.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching admin contact by id");
            return ResponseFactory.Error("An error occurred while fetching the contact.");
        }
    }

    public async Task<ResponseResult> DeleteAdminContactRequestAsync(int id)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient();
            var apiKey = _configuration["AdminApiKey"];
            httpClient.DefaultRequestHeaders.Add("X-Admin-Api-Key", apiKey);

            var response = await httpClient.DeleteAsync($"https://localhost:7297/api/contact/{id}");

            if (response.IsSuccessStatusCode)
            {
                return ResponseFactory.Ok("Contact request deleted successfully.");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to delete contact request. Status code: {response.StatusCode}, Response: {responseBody}");

                return ResponseFactory.Error("Failed to delete contact request. Please try again later.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting admin contact request");
            return ResponseFactory.Error("An error occurred while deleting the contact request.");
        }
    }
}
