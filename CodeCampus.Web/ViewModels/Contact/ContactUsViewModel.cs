using CodeCampus.Web.Models.Components;

namespace CodeCampus.Web.ViewModels.Contact;

public class ContactUsViewModel
{
    public string? Id { get; set; }
    public string HomeLink { get; set; } = null!;
    public string ContactText { get; set; } = null!;
    public string Title { get; set; } = null!;
    public List<LinkComponent> ContactOptions { get; set; } = [];
}
