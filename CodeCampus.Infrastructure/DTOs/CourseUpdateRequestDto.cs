using System.ComponentModel.DataAnnotations;

namespace CodeCampus.Infrastructure.DTOs;

public class CourseUpdateRequestDto
{
    public string? CourseImage { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Title", Prompt = "Enter title", Order = 1)]
    [Required(ErrorMessage = "Title is required")]
    [MinLength(2, ErrorMessage = "A valid title is at least two characters")]
    public string Title { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Author", Prompt = "Enter Author name", Order = 2)]
    [Required(ErrorMessage = "Author is required")]
    [MinLength(2, ErrorMessage = "A valid author name is at least two characters")]
    public string Author { get; set; } = null!;

    [DataType(DataType.Currency)]
    [Display(Name = "Price", Prompt = "Enter price", Order = 3)]
    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10,000.")]
    public decimal Price { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Discount Price", Prompt = "Enter Discount price", Order = 4)]
    [Range(0.00, 10000, ErrorMessage = "Discount Price must be between 0.00 and 10,000.")]
    public decimal? DiscountPrice { get; set; }

    [DataType(DataType.Duration)]
    [Display(Name = "Length in hours", Prompt = "Enter hours", Order = 5)]
    [Required(ErrorMessage = "Hours are required.")]
    [Range(0.1, 1000, ErrorMessage = "Hours must be between 0.1 and 1000.")]
    public double Hours { get; set; }


    [Display(Name = "Likes in percent", Prompt = "Enter percent", Order = 6)]
    [Required(ErrorMessage = "Likes in percent is required.")]
    [Range(0, 100, ErrorMessage = "Likes in percent must be between 0 and 100.")]
    public double LikesInProcent { get; set; }


    [Display(Name = "Likes in numbers", Prompt = "Enter number", Order = 7)]
    [Required(ErrorMessage = "Likes in numbers are required.")]
    [Range(0, 100000, ErrorMessage = "Likes in numbers must be between 0 and 100,000.")]
    public double LikesInNumbers { get; set; }


    [Display(Name = "Is Best Seller", Prompt = "Is this a best seller?", Order = 8)]
    [Required(ErrorMessage = "Please specify if this is a best seller.")]
    public bool IsBestSeller { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Category", Prompt = "Enter category name", Order = 9)]
    [Required(ErrorMessage = "Category is required")]
    [MinLength(2, ErrorMessage = "A valid category name is required")]

    public string CategoryName { get; set; } = null!;
}
