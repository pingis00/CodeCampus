using CodeCampus.Web.Models.Components;

namespace CodeCampus.Web.ViewModels.Account;

public class SavedCoursesViewModel
{
    public string Title { get; set; } = null!;
    public List<CourseComponent> Courses { get; set; } = [];
}
