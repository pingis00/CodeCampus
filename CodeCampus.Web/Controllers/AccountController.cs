using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Interfaces.Services;
using CodeCampus.Infrastructure.Models;
using CodeCampus.Infrastructure.Services;
using CodeCampus.Web.Helpers;
using CodeCampus.Web.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CodeCampus.Web.Controllers;

[Authorize]
public class AccountController(UserManager<UserEntity> userManager, IAddressService addressService, IAccountManager accountManager, SignInManager<UserEntity> signInManager, IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<AccountController> logger) : Controller
{
    private readonly IAccountManager _accountManager = accountManager;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly IAddressService _addressService = addressService;
    private readonly IConfiguration _configuration = configuration;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ILogger<AccountController> _logger = logger;

    [HttpGet]
    [Route("/account/details")]
    public async Task<IActionResult> Details()
    {
        try
        {
            var claims = HttpContext.User.Identities.FirstOrDefault();

            ViewBag.ActiveLink = "Details";
            var viewModel = new AccountDetailsViewModel
            {
                ProfileInfo = await PopulateProfileInfoAsync(),
                BasicInfo = await PopulateBasicInfoAsync(),
                AddressInfo = await PopulateAddressInfoAsync()
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching account details.");
            TempData["Message"] = "Error fetching account details.";
            TempData["MessageType"] = "error";
            return View("Error");
        }
    }

    [HttpPost]
    [Route("/account/details")]
    public async Task<IActionResult> Details(AccountDetailsViewModel viewModel)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Message"] = "User not found.";
                TempData["MessageType"] = "error";
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
                            _logger.LogError("Error updating user basic info: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                            TempData["Message"] = "Something went wrong! Unable to save data";
                            TempData["MessageType"] = "error";
                            return View(viewModel);
                        }
                        else
                        {
                            TempData["Message"] = "Profile updated successfully.";
                            TempData["MessageType"] = "success";
                        }
                    }
                }
            }


            if (viewModel.AddressInfo != null)
            {
                var addressResponse = await _addressService.CreateOrUpdateAddressAsync(user!, viewModel.AddressInfo);
                if (addressResponse.Status != Infrastructure.Responses.StatusCode.OK)
                {
                    _logger.LogError("Error updating user address: {Message}", addressResponse.Message);
                    TempData["Message"] = "Something went wrong! Unable to save address.";
                    TempData["MessageType"] = "error";
                    return View(viewModel);
                }
                else
                {
                    TempData["Message"] = "Address updated successfully.";
                    TempData["MessageType"] = "success";
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating profile.");
            TempData["Message"] = $"Error updating profile: {ex.Message}";
            TempData["MessageType"] = "error";
            return View(viewModel);
        }


        ViewBag.ActiveLink = "Details";
        viewModel.ProfileInfo = await PopulateProfileInfoAsync();
        viewModel.BasicInfo = await PopulateBasicInfoAsync();
        viewModel.AddressInfo = await PopulateAddressInfoAsync();
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
    [Route("/account/change-password")]
    public async Task<IActionResult> ChangePassword(AccountChangePasswordViewModel viewModel)
    {

        try
        {
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        _logger.LogError($"ModelState Error - Key: {state.Key}, Error: {error.ErrorMessage}");
                    }
                }

                TempData["Message"] = "Please correct the errors in the form.";
                TempData["MessageType"] = "error";
                var accountSecurityViewModel = new AccountSecurityViewModel
                {
                    ChangePassword = viewModel,
                    DeleteAccount = new AccountDeleteViewModel()
                };

                return View("AccountSecurity", accountSecurityViewModel);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Message"] = "User not found.";
                TempData["MessageType"] = "error";
                return NotFound();
            }

            var checkPasswordResult = await _userManager.CheckPasswordAsync(user, viewModel.CurrentPassword);
            if (!checkPasswordResult)
            {
                ModelState.AddModelError("SecurityInfo.CurrentPassword", "The current password is incorrect.");
                var accountSecurityViewModel = new AccountSecurityViewModel
                {
                    ChangePassword = viewModel,
                    DeleteAccount = new AccountDeleteViewModel()
                };

                return View("AccountSecurity", accountSecurityViewModel);
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                var accountSecurityViewModel = new AccountSecurityViewModel
                {
                    ChangePassword = viewModel,
                    DeleteAccount = new AccountDeleteViewModel()
                };

                return View("AccountSecurity", accountSecurityViewModel);
            }
            await _userManager.UpdateSecurityStampAsync(user);
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            TempData["Message"] = "Your password has been changed successfully. Please log in with your new password.";
            TempData["MessageType"] = "success";

            return RedirectToAction("SignIn", "Auth");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error changing password.");
            TempData["ErrorMessage"] = ex.Message;
            var accountSecurityViewModel = new AccountSecurityViewModel
            {
                ChangePassword = viewModel,
                DeleteAccount = new AccountDeleteViewModel()
            };

            return View("AccountSecurity", accountSecurityViewModel);
        }
    }

    [HttpPost]
    [Route("/account/delete-account")]
    public async Task<IActionResult> DeleteAccount(AccountDeleteViewModel viewModel)
    {

        try
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Please correct the errors in the form.";
                TempData["MessageType"] = "error";
                var accountSecurityViewModel = new AccountSecurityViewModel
                {
                    ChangePassword = new AccountChangePasswordViewModel(),
                    DeleteAccount = viewModel
                };

                return View("AccountSecurity", accountSecurityViewModel);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Message"] = "User not found.";
                TempData["MessageType"] = "error";
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                _logger.LogError("Error deleting account: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                TempData["Message"] = "Error deleting account.";
                TempData["MessageType"] = "error";
                var accountSecurityViewModel = new AccountSecurityViewModel
                {
                    ChangePassword = new AccountChangePasswordViewModel(),
                    DeleteAccount = viewModel
                };

                return View("AccountSecurity", accountSecurityViewModel);
            }

            await _signInManager.SignOutAsync();
            TempData["Message"] = "Your account has been deleted successfully.";
            TempData["MessageType"] = "success";
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting account.");
            TempData["Message"] = ex.Message;
            TempData["MessageType"] = "error";

            var accountSecurityViewModel = new AccountSecurityViewModel
            {
                ChangePassword = new AccountChangePasswordViewModel(),
                DeleteAccount = viewModel
            };

            return View("AccountSecurity", accountSecurityViewModel);
        }
    }

    [HttpGet]
    [Route("/account/saved-courses")]
    public async Task<IActionResult> SavedCourses()
    {
        try
        {
            var userId = _userManager.GetUserId(User);
            using var http = _httpClientFactory.CreateClient();
            var apiKey = _configuration["ApiKey"];
            http.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

            var response = await http.GetAsync($"https://localhost:7297/api/usercourses/user/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Error fetching saved courses from API.";
                TempData["MessageType"] = "error";
                return View("Error");
            }

            var userCourseDtos = await response.Content.ReadFromJsonAsync<IEnumerable<UserCourseDto>>();

            if (userCourseDtos == null)
            {
                TempData["Message"] = "No saved courses found.";
                TempData["MessageType"] = "error";
                return View("Error");
            }

            var viewModel = new SavedCoursesViewModel
            {
                Courses = userCourseDtos.Select(AdminMappingFactory.MapToCourseComponent).ToList()
            };

            ViewBag.ActiveLink = "SavedCourses";
            ViewData["ShowDeleteButton"] = true;
            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching saved courses.");
            TempData["Message"] = "An error occurred while fetching saved courses.";
            TempData["MessageType"] = "error";
            return View("Error");
        }

    }

    [HttpPost]
    [Route("/account/unenroll-course")]
    public async Task<IActionResult> UnsubscribeCourse(int courseId)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                TempData["Message"] = "User not found.";
                TempData["MessageType"] = "error";
                return RedirectToAction("SavedCourses");
            }

            using var httpClient = _httpClientFactory.CreateClient();
            var apiKey = _configuration["ApiKey"];
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

            var response = await httpClient.DeleteAsync($"https://localhost:7297/api/usercourses/remove?userId={userId}&courseId={courseId}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Error unsubscribing from course: {StatusCode}, {Content}", response.StatusCode, await response.Content.ReadAsStringAsync());
                TempData["Message"] = "An error occurred while unsubscribing from the course.";
                TempData["MessageType"] = "error";
                return RedirectToAction("SavedCourses");
            }

            TempData["Message"] = "You have been unsubscribed from the course.";
            TempData["MessageType"] = "success";
            return RedirectToAction("SavedCourses");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error unsubscribing from course.");
            TempData["Message"] = "An error occurred while unsubscribing from the course.";
            TempData["MessageType"] = "error";
            return RedirectToAction("SavedCourses");
        }
    }

    [HttpPost]
    [Route("/account/delete-all-courses")]
    public async Task<IActionResult> DeleteAllCourses()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                TempData["Message"] = "User not found.";
                TempData["MessageType"] = "error";
                return RedirectToAction("SavedCourses");
            }

            using var httpClient = _httpClientFactory.CreateClient();
            var apiKey = _configuration["ApiKey"];
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

            var response = await httpClient.DeleteAsync($"https://localhost:7297/api/usercourses/user/{userId}/remove-all");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Error deleting all courses: {StatusCode}, {Content}", response.StatusCode, await response.Content.ReadAsStringAsync());
                TempData["Message"] = "An error occurred while deleting all courses.";
                TempData["MessageType"] = "error";
                return RedirectToAction("SavedCourses");
            }

            TempData["Message"] = "All courses have been successfully deleted.";
            TempData["MessageType"] = "success";
            return RedirectToAction("SavedCourses");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting all courses.");
            TempData["Message"] = "An error occurred while deleting all courses.";
            TempData["MessageType"] = "error";
            return RedirectToAction("SavedCourses");
        }

    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        try
        {
            var result = await _accountManager.UploadUserProfileImageAsync(User, file);
            if (!result)
            {
                TempData["ErrorMessage"] = "Failed to upload profile image.";
            }
            else
            {
                TempData["SuccessMessage"] = "Profile image uploaded successfully.";
            }
            return RedirectToAction("Details");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading profile image.");
            TempData["ErrorMessage"] = "An error occurred while uploading profile image.";
            return RedirectToAction("Details");
        }
    }

    private async Task<ProfileInfoViewModel> PopulateProfileInfoAsync()
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null) { return new ProfileInfoViewModel(); }

            return new ProfileInfoViewModel
            {
                FirstName = user!.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                IsExternalAccount = user.IsExternalAccount,
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error populating profile info.");
            return new ProfileInfoViewModel();
        }
    }

    private async Task<AccountDetailsBasicInfoModel> PopulateBasicInfoAsync()
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) { return new AccountDetailsBasicInfoModel(); }

            return new AccountDetailsBasicInfoModel
            {
                UserId = user!.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                Biography = user.Bio
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error populating basic info.");
            return new AccountDetailsBasicInfoModel();
        }

    }

    private async Task<AccountDetailsAddressInfoModel> PopulateAddressInfoAsync()
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            var userAddress = await _addressService.GetUserAddressAsync(user!.Id);

            if (userAddress != null)
            {
                return new AccountDetailsAddressInfoModel
                {
                    Addressline_1 = userAddress.AddressLine_1,
                    Addressline_2 = userAddress.AddressLine_2,
                    PostalCode = userAddress.PostalCode,
                    City = userAddress.City
                };
            }
            else
            {
                return new AccountDetailsAddressInfoModel();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error populating address info.");
            return new AccountDetailsAddressInfoModel();
        }
    }
}
