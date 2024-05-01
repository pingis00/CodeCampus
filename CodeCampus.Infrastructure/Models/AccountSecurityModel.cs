using System.ComponentModel.DataAnnotations;

namespace CodeCampus.Infrastructure.Models;

public class AccountSecurityModel
{
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Current password is required")]
    [Display(Name = "Current Password", Prompt = "Enter your current password", Order = 0)]

    public string CurrentPassword { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "New Password", Prompt = "Enter your new password", Order = 1)]
    [Required(ErrorMessage = " New password is required")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Enter a valid password")]
    public string NewPassword { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm new password", Prompt = "Confirm your new password", Order = 2)]
    [Required(ErrorMessage = "New password must be confirmed")]
    [Compare(nameof(NewPassword), ErrorMessage = "Confirm new password must match new password")]
    public string ConfirmNewPassword { get; set; } = null!;
    public bool DeleteAccount { get; set; } = false;
}
