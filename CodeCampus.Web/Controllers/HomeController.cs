using CodeCampus.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace CodeCampus.Web.Controllers;

public class HomeController(IConfiguration configuration, HttpClient httpClient, ILogger<HomeController> logger) : Controller
{
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<HomeController> _logger = logger;


    public IActionResult Index()
    {
        var viewModel = new HomeIndexViewModel();
        ViewBag.Message = TempData["AccountDeletedMessage"];

        return View(viewModel);
    }

    [Route("/error")]
    public IActionResult Error(int statusCode)
    {
        if (statusCode == 404)
        {
            return View("_404Partial");
        }
        return View("error");
    }

    [Route("/denied")]
    public IActionResult AccessDenied()
    {
        return View("_AccessDenied");
    }

    [HttpPost]
    public async Task<IActionResult> Index(NewsletterViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Invalid input. Please check your details and try again." });
        }
        try
        {
            var apiKey = _configuration["ApiKey"];
            var content = new StringContent(JsonSerializer.Serialize(viewModel), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

            var response = await _httpClient.PostAsync("https://localhost:7043/api/subscribe", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Successfully subscribed!" });
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                return Json(new { success = false, message = "Email is already subscribed." });
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Json(new { success = false, message = "Unauthorized. Please provide a valid API key." });
            }
            else
            {
                return Json(new { success = false, message = "Subscription failed. Please try again later." });
            }
        }
        catch
        {
            return Json(new { success = false, message = "ConnectionFailed" });
        }
    }
}