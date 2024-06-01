using System.ComponentModel.DataAnnotations;

namespace CodeCampus.Web.ViewModels.Account;

public class AccountChangePasswordViewModel
{
    [DataType(DataType.Password)]
    [Display(Name = "Current Password", Prompt = "Enter your current password", Order = 0)]
    [Required(ErrorMessage = "Current password is required")]
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
}
