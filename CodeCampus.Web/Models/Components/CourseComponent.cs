namespace CodeCampus.Web.Models.Components;

public class CourseComponent
{
    public int Id { get; set; }
    public ImageComponent CourseImage { get; set; } = new ImageComponent();
    public string Title { get; set; } = null!;
    public string CourseAuthor { get; set; } = null!;
    public string CoursePrice { get; set; } = null!;
    public string? CourseDiscountPrice { get; set; }
    public string CourseHours { get; set; } = null!;
    public string LikesInProcent { get; set; } = null!;
    public string LikesInNumbers { get; set; } = null!;
    public bool IsBestSeller { get; set; }
    public string CategoryName { get; set; } = null!;
}
