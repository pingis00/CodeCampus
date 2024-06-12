using CodeCampus.Web.Models.Components;

namespace CodeCampus.Web.ViewModels.Home;

public class ShowcaseViewModel
{
    public string? Id { get; set; }
    public ImageComponent ShowcaseImage { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public LinkComponent Link { get; set; } = new LinkComponent();
    public string BrandsText { get; set; } = null!;
    public List<ImageComponent>? Brands { get; set; }
}

