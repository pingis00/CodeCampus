using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Models;

namespace CodeCampus.Infrastructure.Factories;

public static class CourseFactory
{
    public static Course Create(CourseEntity entity)
    {
        return new Course
        {
            Id = entity.Id,
            CourseImage = entity.CourseImage,
            Title = entity.Title,
            Author = entity.Author,
            Price = entity.Price,
            DiscountPrice = entity.DiscountPrice,
            Hours = entity.Hours,
            LikesInProcent = entity.LikesInProcent,
            LikesInNumbers = entity.LikesInNumbers,
            IsBestSeller = entity.IsBestSeller,
            CategoryId = entity.CategoryId,
            Category = entity.Category.CategoryName
        };
    }

    public static Course Create(CourseRequestDto dto)
    {
        return new Course
        {
            CourseImage = dto.CourseImage,
            Title = dto.Title,
            Author = dto.Author,
            Price = dto.Price,
            DiscountPrice = dto.DiscountPrice,
            Hours = dto.Hours,
            LikesInProcent = dto.LikesInProcent,
            LikesInNumbers = dto.LikesInNumbers,
            IsBestSeller = dto.IsBestSeller,
            Category = dto.CategoryName
        };
    }

    public static Course Create(CourseUpdateRequestDto dto)
    {
        return new Course
        {
            CourseImage = dto.CourseImage,
            Title = dto.Title,
            Author = dto.Author,
            Price = dto.Price,
            DiscountPrice = dto.DiscountPrice,
            Hours = dto.Hours,
            LikesInProcent = dto.LikesInProcent,
            LikesInNumbers = dto.LikesInNumbers,
            IsBestSeller = dto.IsBestSeller,
            Category = dto.CategoryName
        };
    }

    public static CourseEntity Create(Course model, int categoryId)
    {
        return new CourseEntity
        {
            CourseImage = model.CourseImage,
            Title = model.Title,
            Author = model.Author,
            Price = model.Price,
            DiscountPrice = model.DiscountPrice,
            Hours = model.Hours,
            LikesInProcent = model.LikesInProcent,
            LikesInNumbers = model.LikesInNumbers,
            IsBestSeller = model.IsBestSeller,
            CategoryId = categoryId
        };
    }
}
