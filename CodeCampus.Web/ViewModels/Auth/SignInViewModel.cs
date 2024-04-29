using CodeCampus.Infrastructure.Models;

namespace CodeCampus.Web.ViewModels.Auth;

public class SignInViewModel
{
    public string Title { get; set; } = "Sign in";
    public SignInModel Form { get; set; } = new SignInModel();
    public string? ErrorMessage { get; set; }
}
