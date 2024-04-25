using CodeCampus.Web.Models.Components;

namespace CodeCampus.Web.ViewModels.Home;

public class FeaturesViewModel
{
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public List<IconComponent> IconFeatures { get; set; } = [];
}
