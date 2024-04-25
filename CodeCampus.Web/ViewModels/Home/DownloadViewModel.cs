using CodeCampus.Web.Models.Components;

namespace CodeCampus.Web.ViewModels.Home;

public class DownloadViewModel
{
    public string? Id { get; set; }
    public ImageComponent MobileImage { get; set; } = new ImageComponent();
    public string Title { get; set; } = null!;
    public List<AppLinkComponent> AppLinks { get; set; } = [];

    public class AppLinkComponent
    {
        public string StoreName { get; set; } = null!;
        public StarRatingComponent StarRating { get; set; } = new StarRatingComponent();
        public string EditorChoiceText { get; set; } = null!;
        public string RatingText { get; set; } = null!;
        public LinkComponent Link { get; set; } = new LinkComponent();
    }
}
