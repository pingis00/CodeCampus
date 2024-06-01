using CodeCampus.Infrastructure.Models;

namespace CodeCampus.Web.ViewModels.Account;

public class AccountSecurityViewModel
{
    public AccountChangePasswordViewModel ChangePassword { get; set; } = new AccountChangePasswordViewModel();
    public AccountDeleteViewModel DeleteAccount { get; set; } = new AccountDeleteViewModel();
}
