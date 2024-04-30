using CodeCampus.Web.ViewModels.Contact;
using Microsoft.AspNetCore.Mvc;

namespace CodeCampus.Web.Controllers;

public class ContactController(IConfiguration configuration) : Controller
{
    private readonly IConfiguration _configuration = configuration;

    [HttpGet]
    public IActionResult Contact()
    {
        ViewData["GoogleMapsApiKey"] = _configuration["GoogleMapsApiKey"];
        var viewModel = new ContactViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult SubmitContactForm(ContactViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("ThankYou");
        }

        return View("Contact", viewModel);
    }
}
