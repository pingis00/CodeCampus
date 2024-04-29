using CodeCampus.Web.ViewModels.Auth;
using CodeCampus.Web.ViewModels.Contact;
using Microsoft.AspNetCore.Mvc;

namespace CodeCampus.Web.Controllers;

public class AuthController : Controller
{
    [Route("/signup")]
    public IActionResult SignUp()
    {
        var viewModel = new SignUpViewModel();
        return View(viewModel);
    }

    [Route("/signin")]
    public IActionResult SignIn()
    {
        var viewModel = new SignInViewModel();
        return View(viewModel);
    }
}
