using CodeCampus.Web.Models.Components;

namespace CodeCampus.Web.ViewModels.Account;

public class ProfileInfoViewModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsExternalAccount { get; set; }
    public ImageComponent UserProfileImage { get; set; } = new ImageComponent();
}
