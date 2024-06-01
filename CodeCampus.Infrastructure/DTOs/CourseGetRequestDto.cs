﻿namespace CodeCampus.Infrastructure.DTOs;

public class CourseGetRequestDto
{
    public int Id { get; set; }
    public string? CourseImage { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public double Hours { get; set; }

    public double LikesInProcent { get; set; }

    public double LikesInNumbers { get; set; }

    public bool IsBestSeller { get; set; }

    public string Category { get; set; } = null!;

    public int CategoryId { get; set; }

}
