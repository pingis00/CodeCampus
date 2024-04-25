using CodeCampus.Infrastructure.Models;
using CodeCampus.Web.Models.Components;

namespace CodeCampus.Web.ViewModels.Home;

public class NewsletterViewModel
{
    public string? Id { get; set; }
    public string Title { get; set; } = null!;
    public ImageComponent Image { get; set; } = new ImageComponent();
    public string SubTitle { get; set; } = null!;
    public List<NewsletterOption> NewsletterOptions { get; set; } = [];
    public NewsletterSignupModel NewsletterSignup { get; set; } = new NewsletterSignupModel();
}

public class NewsletterOption
{
    public string DisplayName { get; set; } = null!;
    public bool IsChecked { get; set; }
}
