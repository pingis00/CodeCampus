using CodeCampus.Infrastructure.Models;

namespace CodeCampus.Web.ViewModels.Account;

public class AccountDetailsViewModel
{
    public string Title { get; set; } = "Account Details";

    public ProfileInfoViewModel ProfileInfo { get; set; } = null!;
    public AccountDetailsBasicInfoModel BasicInfo { get; set; } = null!;
    public AccountDetailsAddressInfoModel AddressInfo { get; set; } = null!;
}
