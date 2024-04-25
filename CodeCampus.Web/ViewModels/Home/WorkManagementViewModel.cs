using CodeCampus.Web.Models.Components;

namespace CodeCampus.Web.ViewModels.Home;

public class WorkManagementViewModel
{
    public string? Id { get; set; }
    public ImageComponent Image { get; set; } = new ImageComponent();
    public string Title { get; set; } = null!;
    public List<IconComponent> Features { get; set; } = [];
    public LinkComponent Link { get; set; } = new LinkComponent();
}
