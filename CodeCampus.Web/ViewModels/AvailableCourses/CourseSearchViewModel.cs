namespace CodeCampus.Web.ViewModels.AvailableCourses;

public class CourseSearchViewModel
{
    public string? Id { get; set; }
    public string HomeLink { get; set; } = null!;
    public string CoursesText { get; set; } = null!;
    public string Title { get; set; } = null!;
    public List<CategoryViewModel> Categories { get; set; } = [];
    public string SelectedCategory { get; set; } = null!;
    public string SearchQuery { get; set; } = null!;
}
