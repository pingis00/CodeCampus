using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Interfaces.Services.Admin;
using CodeCampus.Infrastructure.Services.Admin;
using CodeCampus.Web.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace CodeCampus.Web.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, ILogger<AuthController> logger, ITokenService tokenService) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly ILogger<AuthController> _logger = logger;
    private readonly ITokenService _tokenService = tokenService;

    [HttpGet]
    [Route("/signup")]
    public IActionResult SignUp()
    {
        if (_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("Details", "Account");
        }
        return View();
    }

    [HttpPost]
    [Route("/signup")]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var exists = await _userManager.Users.AnyAsync(x => x.Email == viewModel.Form.Email);
                if (exists)
                {
                    ModelState.AddModelError("AlreadyExists", "User with the same email address already exists");
                    ViewData["ErrorMessage"] = "User with the same email address already exists";
                    return View();
                }

                var userEntity = new UserEntity
                {
                    FirstName = viewModel.Form.FirstName,
                    LastName = viewModel.Form.LastName,
                    Email = viewModel.Form.Email,
                    UserName = viewModel.Form.Email
                };

                var result = await _userManager.CreateAsync(userEntity, viewModel.Form.Password);
                if (result.Succeeded)
                {
                    TempData["Message"] = "Account created successfully. Please sign in.";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("SignIn", "Auth");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during sign-up.");
                ModelState.AddModelError("", $"Unexpected error occurred: {ex.Message}");
                ViewData["ErrorMessage"] = $"Unexpected error occurred: {ex.Message}";
            }
        }
        return View(viewModel);
    }

    [Route("/signin")]
    public IActionResult SignIn(string returnUrl)
    {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Details", "Account");

        ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/");
        return View();
    }

    [Route("/signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel, string returnUrl)
    {
        ModelState.Clear();

        if (string.IsNullOrWhiteSpace(viewModel.Form.Email) || string.IsNullOrWhiteSpace(viewModel.Form.Password))
        {
            ModelState.AddModelError("IncorrectValues", "Email and password are required");
            ViewData["ErrorMessage"] = "Email and password are required";
            return View(viewModel);
        }

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            var result = await _signInManager.PasswordSignInAsync(viewModel.Form.Email, viewModel.Form.Password, viewModel.Form.RememberMe, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(viewModel.Form.Email);

                if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    var token = await _tokenService.GenerateToken(user);

                    Response.Cookies.Append("access_token", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    });
                }

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Details", "Account");
            }

            ModelState.AddModelError("IncorrectValues", "Incorrect email or password");
            TempData["Message"] = "Incorrect email or password";
            TempData["MessageType"] = "error";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Sign in failed.");
            ModelState.AddModelError("", $"Sign in failed: {ex.Message}");
            ViewData["ErrorMessage"] = $"Sign in failed: {ex.Message}";
        }

        ViewData["ErrorMessage"] = "Incorrect email or password";
        return View(viewModel);
    }

    [HttpGet]
    [Route("/signout")]
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        TempData["Message"] = "You have been signed out.";
        TempData["MessageType"] = "success";
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Facebook()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("facebookCallback"));
        return new ChallengeResult("Facebook", authProps);
    }

    [HttpGet]
    public async Task<IActionResult> FacebookCallback()
    {
        try
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info != null)
            {
                var userEntity = new UserEntity
                {
                    FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                    LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                    IsExternalAccount = true
                };

                var user = await _userManager.FindByEmailAsync(userEntity.Email);
                if (user == null)
                {
                    var result = await _userManager.CreateAsync(userEntity);
                    if (result.Succeeded)
                    {
                        user = await _userManager.FindByEmailAsync(userEntity.Email);
                    }
                }

                if (user != null)
                {
                    if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                    {
                        user.FirstName = userEntity.FirstName;
                        user.LastName = userEntity.LastName;
                        user.Email = userEntity.Email;

                        await _userManager.UpdateAsync(user);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (HttpContext.User != null)
                        return RedirectToAction("Details", "Account");
                }
            }

            TempData["Message"] = "Failed to authenticate with Facebook.";
            TempData["MessageType"] = "error";
            return RedirectToAction("SignIn", "Auth");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Facebook authentication failed.");
            TempData["Message"] = "Facebook authentication failed.";
            TempData["MessageType"] = "error";
            return RedirectToAction("SignIn", "Auth");
        }
    }

    [HttpGet]
    public IActionResult Google()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action("googleCallback"));
        return new ChallengeResult("Google", authProps);
    }

    [HttpGet]
    public async Task<IActionResult> GoogleCallback()
    {
        try
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info != null)
            {
                var userEntity = new UserEntity
                {
                    FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                    LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                    IsExternalAccount = true
                };

                var user = await _userManager.FindByEmailAsync(userEntity.Email);
                if (user == null)
                {
                    var result = await _userManager.CreateAsync(userEntity);
                    if (result.Succeeded)
                    {
                        user = await _userManager.FindByEmailAsync(userEntity.Email);
                    }
                }

                if (user != null)
                {
                    if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                    {
                        user.FirstName = userEntity.FirstName;
                        user.LastName = userEntity.LastName;
                        user.Email = userEntity.Email;

                        await _userManager.UpdateAsync(user);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (HttpContext.User != null)
                        return RedirectToAction("Details", "Account");
                }
            }

            TempData["Message"] = "Failed to authenticate with Google.";
            TempData["MessageType"] = "error";
            return RedirectToAction("SignIn", "Auth");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Google authentication failed.");
            TempData["Message"] = "Google authentication failed.";
            TempData["MessageType"] = "error";
            return RedirectToAction("SignIn", "Auth");
        }
    }
}

