using CodeCampus.Infrastructure.Models;

namespace CodeCampus.Web.ViewModels.Auth;

public class SignUpViewModel
{
    public string PageTitle { get; set; } = "Sign up";
    public SignUpModel Form { get; set; } = new SignUpModel();
    public bool TermsAndConditions { get; set; } = false;
}
