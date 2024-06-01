namespace CodeCampus.Web.ViewModels.Account;

public class CourseModel
{
    public int Id { get; set; }
    public string? CourseImage { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public string Hours { get; set; } = null!;
    public string LikesInProcent { get; set; } = null!;
    public string LikesInNumbers { get; set; } = null!;
    public bool IsBestSeller { get; set; }
    public string CategoryName { get; set; } = null!;
}
