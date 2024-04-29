using CodeCampus.Web.Models.Components;

namespace CodeCampus.Web.ViewModels.AvailableCourses;

public class GetStartedViewModel
{
    public string? Id { get; set; }
    public string PreHighlightTitle { get; set; } = null!;
    public string HighlightedWord { get; set; } = null!;
    public string PostHighlightTitle { get; set; } = null!;
    public string SubTitle { get; set; } = null!;
    public LinkComponent CtaButton { get; set; } = null!;
    public ImageComponent Image { get; set; } = null!;
}
