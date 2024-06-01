using System.ComponentModel.DataAnnotations;

namespace CodeCampus.Infrastructure.Models;

public class NewsletterSignupModel
{
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email address", Prompt = "Your Email")]
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$", ErrorMessage = "Enter a valid email")]
    public string Email { get; set; } = null!;
    public bool IsSubscribed { get; set; } = false;
}

