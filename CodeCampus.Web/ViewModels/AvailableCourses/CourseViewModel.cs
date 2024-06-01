using CodeCampus.Web.Models.Components;

namespace CodeCampus.Web.ViewModels.AvailableCourses;

public class CourseViewModel
{
    public int Id { get; set; }
    public ImageComponent? CourseImage { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public Decimal Price { get; set; }
    public Decimal? DiscountPrice { get; set; }
    public double Hours { get; set; }
    public double LikesInProcent { get; set; }
    public double LikesInNumbers { get; set; }
    public bool IsBestSeller { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; } = null!;

}
