using CodeCampus.Infrastructure.Models;
using CodeCampus.Infrastructure.Responses;

namespace CodeCampus.Infrastructure.Interfaces.Services;

public interface ICourseService
{
    Task<ResponseResult> CreateCourseAsync(Course request);
    Task<ResponseResult> GetCourseByIdAsync(int id);
    Task<ResponseResult> GetAllCoursesAsync();
    Task<ResponseResult> UpdateCourseAsync(int id, Course request);
    Task<ResponseResult> DeleteCourseAsync(int id);
}
