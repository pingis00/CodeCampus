namespace CodeCampus.Infrastructure.DTOs;

public class UserCourseDto
{
    public int CourseId { get; set; }
    public string Title { get; set; } = null!;
    public string? CourseImage { get; set; }
    public string CourseAuthor { get; set; } = null!;
    public decimal CoursePrice { get; set; }
    public decimal? CourseDiscountPrice { get; set; }
    public double CourseHours { get; set; }
    public double LikesInProcent { get; set; }
    public double LikesInNumbers { get; set; }
    public bool IsBestSeller { get; set; }
    public string CategoryName { get; set; } = null!;
}
