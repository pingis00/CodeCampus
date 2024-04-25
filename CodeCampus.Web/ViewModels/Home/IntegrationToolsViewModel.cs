using CodeCampus.Web.Models.Components;

namespace CodeCampus.Web.ViewModels.Home;

public class IntegrationToolsViewModel
{
    public string? Id { get; set; }
    public string Title { get; set; } = null!;
    public string Subtitle { get; set; } = null!;
    public List<IconComponent> Tools { get; set; } = [];
}
