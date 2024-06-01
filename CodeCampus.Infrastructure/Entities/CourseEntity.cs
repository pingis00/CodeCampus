namespace CodeCampus.Infrastructure.Entities;

public class CourseEntity
{
    public int Id { get; set; }
    public string? CourseImage { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public Decimal Price { get; set; }
    public Decimal? DiscountPrice { get; set; }
    public double Hours { get; set; }
    public double LikesInProcent { get; set;}
    public double LikesInNumbers { get; set; }
    public bool IsBestSeller { get; set; }

    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!;

    public ICollection<UserCourseEntity> UserCourses { get; set; } = [];

}
