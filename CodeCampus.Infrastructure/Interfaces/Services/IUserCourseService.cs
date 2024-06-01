using CodeCampus.Infrastructure.Responses;

namespace CodeCampus.Infrastructure.Interfaces.Services;

public interface IUserCourseService
{
    Task<ResponseResult> AddUserCourseAsync(string userId, int courseId);
    Task<ResponseResult> RemoveUserCourseAsync(string userId, int courseId);
    Task<ResponseResult> GetUserCoursesAsync(string userId);
    Task<ResponseResult> RemoveAllUserCoursesAsync(string userId);
}
