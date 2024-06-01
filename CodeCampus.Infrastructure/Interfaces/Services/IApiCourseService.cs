using CodeCampus.Infrastructure.Responses;

namespace CodeCampus.Infrastructure.Interfaces.Services;

public interface IApiCourseService
{
    Task<ResponseResult> GetAllCoursesAsync();
    Task<ResponseResult> GetCourseByIdAsync(int id);
}
