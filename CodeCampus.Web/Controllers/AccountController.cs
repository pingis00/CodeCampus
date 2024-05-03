using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Services;
using CodeCampus.Web.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
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
            if (addressResponse.Status != Infrastructure.Responses.StatusCode.OK)
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

    [HttpPost]
    [Route("/account/account-security")]
    public async Task<IActionResult> AccountSecurity(AccountSecurityViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var checkPasswordResult = await _userManager.CheckPasswordAsync(user, viewModel.SecurityInfo.CurrentPassword);
        if (!checkPasswordResult)
        {
            ModelState.AddModelError("SecurityInfo.CurrentPassword", "The current password is incorrect.");
            return View(viewModel);
        }

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, viewModel.SecurityInfo.CurrentPassword, viewModel.SecurityInfo.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            foreach (var error in changePasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(viewModel);
        }
        await _userManager.UpdateSecurityStampAsync(user);
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

        TempData["SuccessMessage"] = "Your password has been changed successfully. Please log in with your new password.";

        return RedirectToAction("SignIn", "Auth");
    }

    [Route("/account/saved-courses")]
    public IActionResult SavedCourses()
    {
        ViewBag.ActiveLink = "SavedCourses";
        var viewModel = new SavedCoursesViewModel();
        return View(viewModel);
    }
}

//[HttpPost]
//[Route("/account/account-security")]
//public IActionResult DeleteAccount(AccountSecurityViewModel viewModel)
//{

//    return View(viewModel);
//}
