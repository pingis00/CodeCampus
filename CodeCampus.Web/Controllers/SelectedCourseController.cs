using CodeCampus.Web.ViewModels.SelectedCourse;
using Microsoft.AspNetCore.Mvc;

namespace CodeCampus.Web.Controllers;

public class SelectedCourseController : Controller
{
    [Route("/selectedcourse")]
    public IActionResult Index()
    {
        var viewModel = new SelectedCourseViewModel();

        return View(viewModel);
    }
}
