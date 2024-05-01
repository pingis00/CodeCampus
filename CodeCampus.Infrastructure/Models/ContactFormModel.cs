using System.ComponentModel.DataAnnotations;

namespace CodeCampus.Infrastructure.Models;

public class ContactFormModel
{
    [DataType(DataType.Text)]
    [Display(Name = "Full name", Prompt = "Enter your full name", Order = 0)]
    [Required(ErrorMessage = "Full name is required")]
    [MinLength(2, ErrorMessage = "Enter a valid name")]
    public string FullName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 1)]
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$", ErrorMessage = "Enter a valid email")]
    public string Email { get; set; } = null!;

    [Display(Name = "Service", Prompt = "Choose the service you are interested in", Order = 2)]
    public string? Service { get; set; }

    [Display(Name = "Message", Prompt = "Enter your message here...", Order = 3)]
    [DataType(DataType.MultilineText)]
    [Required(ErrorMessage = "Message is required")]
    [MinLength(10, ErrorMessage = "At least 10 characters required")]

    public string? Message { get; set; }
}
