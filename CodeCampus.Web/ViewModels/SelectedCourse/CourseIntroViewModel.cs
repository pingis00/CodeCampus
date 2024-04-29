using CodeCampus.Web.Models.Components;

namespace CodeCampus.Web.ViewModels.SelectedCourse;

public class CourseIntroViewModel
{
    public string? Id { get; set; }
    public ImageComponent BackgroundImage { get; set; } = null!;
    public LinkComponent HomeLink { get; set; } = new LinkComponent();
    public LinkComponent CoursesLink { get; set; } = new LinkComponent();
    public string CourseView { get; set; } = null!;
    public List<string> Tags { get; set; } = [];
    public string CourseTitle { get; set; } = null!;
    public string CourseDescription { get; set; } = null!;
    public StarRatingComponent StarRating { get; set; } = new StarRatingComponent();
    public string Reviews { get; set; } = null!;
    public string Likes { get; set; } = null!;
    public string CourseHours { get; set; } = null!;
    public ImageComponent AuthorImage { get; set; } = new ImageComponent();
    public string AuthorText { get; set; } = null!;
    public string AuthorName { get; set; } = null!;
}
