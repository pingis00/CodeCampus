using CodeCampus.Web.ViewModels.AvailableCourses;
using Microsoft.AspNetCore.Mvc;

namespace CodeCampus.Web.Controllers;

public class AvailableCoursesController : Controller
{
    [Route("/availablecourses")]
    public IActionResult Index()
    {
        var viewModel = new AvailableCoursesViewModel();

        return View(viewModel);
    }
}
