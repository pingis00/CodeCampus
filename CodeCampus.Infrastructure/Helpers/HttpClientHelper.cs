using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace CodeCampus.Infrastructure.Helpers;

public class HttpClientHelper(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, ILogger<HttpClientHelper> logger)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ILogger<HttpClientHelper> _logger = logger;


    public HttpClient CreateHttpClientWithAdminApiKeyAndToken()
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var token = GetTokenFromCookie();
            var adminApiKey = GetAdminApiKeyFromCookie();

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token or Admin API key is missing.");
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpClient.DefaultRequestHeaders.Add("X-Admin-Api-Key", adminApiKey);
            return httpClient;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the HttpClient with admin API key and token.");
            throw;
        }
    }

    public HttpClient CreateHttpClientWithApiKey()
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var apiKey = GetApiKeyFromCookie();
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new UnauthorizedAccessException("API key is missing.");
            }
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
            return httpClient;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the HttpClient with API key.");
            throw;
        }
    }

    public string GetTokenFromCookie()
    {
        try
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["access_token"];
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token is missing.");
            }
            return token!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the token from cookie.");
            throw;
        }
    }

    public string GetAdminApiKeyFromCookie()
    {
        try
        {
            var adminApiKey = _httpContextAccessor.HttpContext?.Request.Cookies["admin_api_key"];
            if (string.IsNullOrEmpty(adminApiKey))
            {
                throw new UnauthorizedAccessException("Admin API key is missing.");
            }
            return adminApiKey!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the admin API key from cookie.");
            throw;
        }
    }

    public string GetApiKeyFromCookie()
    {
        try
        {
            var apiKey = _httpContextAccessor.HttpContext?.Request.Cookies["api_key"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new UnauthorizedAccessException("API key is missing.");
            }
            return apiKey!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the API key from cookie.");
            throw;
        }

    }
}
