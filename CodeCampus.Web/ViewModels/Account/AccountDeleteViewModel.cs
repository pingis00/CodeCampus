using CodeCampus.Infrastructure.Helpers;

namespace CodeCampus.Web.ViewModels.Account;

public class AccountDeleteViewModel
{
    [CheckBoxRequired(ErrorMessage = "You must confirm that you want to delete your account.")]
    public bool DeleteAccount { get; set; } = false;
}
