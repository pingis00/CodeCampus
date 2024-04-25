using CodeCampus.Web.Models.Components;

namespace CodeCampus.Web.ViewModels.Home;

public class ThemeSwitchViewModel
{
    public string? Id { get; set; }
    public string TitleLeft { get; set; } = null!;
    public string TitleRight { get; set; } = null!;
    public ImageComponent Image { get; set; } = new ImageComponent();
    public ImageComponent SwitchButton { get; set; } = new ImageComponent();
}
