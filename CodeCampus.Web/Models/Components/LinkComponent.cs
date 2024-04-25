namespace CodeCampus.Web.Models.Components;

public class LinkComponent
{
    public string Title { get; set; } = null!;
    public string ControllerName { get; set; } = null!;
    public string ActionName { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Text { get; set; } = null!;
    public ImageComponent Icon { get; set; } = new ImageComponent();
}
