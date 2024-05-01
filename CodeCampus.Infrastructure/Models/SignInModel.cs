using System.ComponentModel.DataAnnotations;

namespace CodeCampus.Infrastructure.Models;

public class SignInModel
{
    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 0)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$", ErrorMessage = "Enter a valid email")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Enter your password", Order = 1)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Enter a valid password")]
    public string Password { get; set; } = null!;

    [Display(Name = "Remember me", Order = 2)]
    public bool RememberMe { get; set; }
}
