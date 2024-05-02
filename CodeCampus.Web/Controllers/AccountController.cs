using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Services;
using CodeCampus.Web.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeCampus.Web.Controllers;

public class AccountController(UserManager<UserEntity> userManager, AddressService addressService) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AddressService _addressService = addressService;

    [Route("/account/details")]
    public IActionResult Details()
    {
        ViewBag.ActiveLink = "Details";
        var viewModel = new AccountDetailsViewModel();

        return View(viewModel);
    }

    [HttpPost]
    [Route("/account/details")]
    public async Task<IActionResult> Details(AccountDetailsViewModel viewModel)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        if (viewModel.BasicInfo != null)
        {
            if (viewModel.BasicInfo.FirstName != null && viewModel.BasicInfo.LastName != null && viewModel.BasicInfo.Email != null)
            {
                if (user != null)
                {
                    user.FirstName = viewModel.BasicInfo.FirstName;
                    user.LastName = viewModel.BasicInfo.LastName;
                    user.Email = viewModel.BasicInfo.Email;
                    user.PhoneNumber = viewModel.BasicInfo.PhoneNumber;
                    user.Bio = viewModel.BasicInfo.Biography;

                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("IncorrectValues", "Something went wrong! Unable to save data");
                        ViewData["ErrorMessage"] = "Something went wrong! Unable to save data";
                    }
                }
            }
        }


        if (viewModel.AddressInfo != null)
        {
            var addressResponse = await _addressService.CreateOrUpdateAddressAsync(user!, viewModel.AddressInfo);
            if (addressResponse.Status != CodeCampus.Infrastructure.Responses.StatusCode.OK)
            {
                ModelState.AddModelError("IncorrectValues", "Something went wrong! Unable to save data");
                ViewData["ErrorMessage"] = "Something went wrong! Unable to save data";
            }
        }
        ViewBag.ActiveLink = "Details";
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
