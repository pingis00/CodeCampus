using CodeCampus.Infrastructure.Helpers;
using CodeCampus.Web.ViewModels.Contact;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace CodeCampus.Web.Controllers;

public class ContactController(HttpClientHelper httpClientHelper, IConfiguration configuration, ILogger<ContactController> logger) : Controller
{
    private readonly HttpClientHelper _httpClientHelper = httpClientHelper;
    private readonly IConfiguration _configuration = configuration;
    private readonly ILogger<ContactController> _logger = logger;

    [HttpGet]
    public IActionResult Contact()
    {
        ViewData["GoogleMapsApiKey"] = _configuration["GoogleMapsApiKey"];
        var viewModel = new ContactViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Contact(ContactFormViewModel viewModel)
    {

        if (!ModelState.IsValid)
        {
            TempData["Message"] = "Invalid input. Please check your details and try again.";
            TempData["MessageType"] = "error";
            return RedirectToAction("Contact");
        }

        try
        {
            var httpClient = _httpClientHelper.CreateHttpClientWithApiKey();
            var content = new StringContent(JsonSerializer.Serialize(viewModel.Form), Encoding.UTF8, "application/json");


            var response = await httpClient.PostAsync("https://localhost:7297/api/contact", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Contact request submitted successfully.";
                TempData["MessageType"] = "success";
                return RedirectToAction("Contact");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                TempData["Message"] = "Unauthorized. Please provide a valid API key.";
                TempData["MessageType"] = "error";
            }
            else
            {
                TempData["Message"] = "Failed to submit contact request. Please try again later.";
                TempData["MessageType"] = "error";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error submitting contact request.");
            TempData["Message"] = "An unexpected error occurred. Please try again later.";
            TempData["MessageType"] = "error";
        }
        return View(viewModel);
    }
}
