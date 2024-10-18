using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Helpers;
using CodeCampus.Infrastructure.Interfaces.Services.Admin;
using CodeCampus.Infrastructure.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace CodeCampus.Infrastructure.Services.Admin;

public class AdminContactService(HttpClientHelper httpClientHelper, ILogger<AdminContactService> logger) : IAdminContactService
{
    private readonly ILogger<AdminContactService> _logger = logger;
    private readonly HttpClientHelper _httpClientHelper = httpClientHelper;

    public async Task<ResponseResult> GetAllAdminContactsAsync()
    {
        try
        {
            var httpClient = _httpClientHelper.CreateHttpClientWithAdminApiKeyAndToken();

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
            var httpClient = _httpClientHelper.CreateHttpClientWithAdminApiKeyAndToken();

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
            var httpClient = _httpClientHelper.CreateHttpClientWithAdminApiKeyAndToken();

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
