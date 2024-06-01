namespace CodeCampus.Web.Models.Components;

public class IconComponent
{
    public ImageComponent Icon { get; set; } = new ImageComponent();
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}