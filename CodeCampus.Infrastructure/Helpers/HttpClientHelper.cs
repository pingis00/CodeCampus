using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace CodeCampus.Infrastructure.Helpers;

public class HttpClientHelper(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, ILogger<HttpClientHelper> logger)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ILogger<HttpClientHelper> _logger = logger;


    public HttpClient CreateHttpClientWithToken()
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var token = GetTokenFromCookie();
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token is missing.");
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return httpClient;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the HttpClient with token.");
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
}
