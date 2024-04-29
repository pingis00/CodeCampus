using CodeCampus.Infrastructure.Models;

namespace CodeCampus.Web.ViewModels.Account;

public class AccountSecurityViewModel
{
    public string Title { get; set; } = "Account Security";
    public AccountSecurityModel SecurityInfo { get; set; } = new AccountSecurityModel();
}
