using CodeCampus.Web.Models.Components;
namespace CodeCampus.Web.ViewModels.Contact;

public class ContactViewModel
{
    public string? Id { get; set; } = "contact-us";
    public string HomeLink { get; set; } = "Home";
    public string ContactText { get; set; } = "Contact";
    public string HeadTitle { get; set; } = "Contact Us";

    public List<LinkComponent> ContactOptions { get; set; } =
    [
        new LinkComponent()
        {
            Title = "Email us",
            Text = "Please feel free to drop us a line. We will respond as soon as possible.",
            ActionName = "Leave a message",
            Url = "*",
            Icon = new ImageComponent {
                ImageUrl = "/Assets/Icons/ui/email-icon-light.svg",
                DarkModeImageUrl = "/Assets/Icons/ui/email-icon-dark.svg",
                AltText = "icon of a letter"
            }
        },
        new LinkComponent()
        {
            Title = "Careers",
            Text = "Sit ac ipsum leo lorem magna nunc mattis maecenas non vestibulum.",
            ActionName = "Send an application",
            Url = "*",
            Icon = new ImageComponent {
                ImageUrl = "/Assets/Icons/ui/career-icon-light.svg",
                DarkModeImageUrl = "/Assets/Icons/ui/career-icon-dark.svg",
                AltText = ""
            }
        }
    ];
}
