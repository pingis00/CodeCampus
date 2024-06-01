using CodeCampus.Web.Models.Components;

namespace CodeCampus.Web.ViewModels.SelectedCourse;

public class InstructorInfoViewModel
{
    public ImageComponent InstructorImage { get; set; } = new ImageComponent();
    public string Title { get; set; } = null!;
    public string InstructorName { get; set; } = null!;
    public string Biography { get; set; } = null!;
    public LinkComponent YoutubeLink { get; set; } = new LinkComponent();
    public LinkComponent FacebookLink { get; set; } = new LinkComponent();
}

