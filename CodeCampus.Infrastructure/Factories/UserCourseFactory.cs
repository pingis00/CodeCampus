using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Entities;

namespace CodeCampus.Infrastructure.Factories;

public class UserCourseFactory
{
    public static UserCourseDto Create(UserCourseEntity entity)
    {
        return new UserCourseDto
        {
            CourseId = entity.CourseId,
            Title = entity.Course.Title,
            CourseImage = entity.Course.CourseImage,
            CourseAuthor = entity.Course.Author,
            CoursePrice = entity.Course.Price,
            CourseDiscountPrice = entity.Course.DiscountPrice,
            CourseHours = entity.Course.Hours,
            LikesInProcent = entity.Course.LikesInProcent,
            LikesInNumbers = entity.Course.LikesInNumbers,
            IsBestSeller = entity.Course.IsBestSeller,
            CategoryName = entity.Course.Category.CategoryName
        };
    }
}
