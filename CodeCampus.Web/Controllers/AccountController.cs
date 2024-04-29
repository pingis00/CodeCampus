using CodeCampus.Web.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace CodeCampus.Web.Controllers;

public class AccountController : Controller
{
    [Route("/account/details")]
    public IActionResult Details()
    {
        ViewBag.ActiveLink = "Details";
        var viewModel = new AccountDetailsViewModel();

        return View(viewModel);
    }

    [Route("/account/account-security")]
    public IActionResult AccountSecurity()
    {
        ViewBag.ActiveLink = "Security";
        var viewModel = new AccountSecurityViewModel();

        return View(viewModel);
    }


    [Route("/account/saved-courses")]
    public IActionResult SavedCourses()
    {
        ViewBag.ActiveLink = "SavedCourses";
        var viewModel = new SavedCoursesViewModel();
        return View(viewModel);
    }
}
