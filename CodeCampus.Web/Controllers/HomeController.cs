using CodeCampus.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace CodeCampus.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var viewModel = new HomeIndexViewModel();

        return View(viewModel);
    }

    public IActionResult Error(int statusCode)
    {
        if (statusCode == 404)
        {
            return View("_404Partial");
        }
        return View("error");
    }
}
