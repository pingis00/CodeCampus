namespace CodeCampus.Web.Models.Components;

public class CourseComponent
{
    public int Id { get; set; }
    public ImageComponent CourseImage { get; set; } = new ImageComponent();
    public string Title { get; set; } = null!;
    public string CourseAuthor { get; set; } = null!;
    public string CoursePrice { get; set; } = null!;
    public string CourseHours { get; set; } = null!;
    public string Likes { get; set; } = null!;
}
