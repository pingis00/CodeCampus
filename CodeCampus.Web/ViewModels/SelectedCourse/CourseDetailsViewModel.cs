using CodeCampus.Web.Models.Components;

namespace CodeCampus.Web.ViewModels.SelectedCourse;

public class CourseDetailsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<IconComponent> LearningOutcomes { get; set; } = [];
    public CourseIncludes Includes { get; set; } = new CourseIncludes();
    public List<ProgramDetail> ProgramDetails { get; set; } = [];
}

public class CourseIncludes
{
    public string Title { get; set; } = null!;
    public string OriginalPrice { get; set; } = null!;
    public string SalePrice { get; set; } = null!;
    public List<IconComponent> Icon { get; set; } = [];
    public LinkComponent JoinCourseButton { get; set; } = new LinkComponent();
}

public class ProgramDetail
{
    public string Number { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}
