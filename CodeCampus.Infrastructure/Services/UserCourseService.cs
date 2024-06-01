using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Interfaces.Repositories;
using CodeCampus.Infrastructure.Interfaces.Services;
using CodeCampus.Infrastructure.Responses;
using Microsoft.Extensions.Logging;

namespace CodeCampus.Infrastructure.Services;

public class UserCourseService(IUserCourseRepository userCourseRepository, ILogger<UserCourseService> logger) : IUserCourseService
{
    private readonly IUserCourseRepository _userCourseRepository = userCourseRepository;
    private readonly ILogger<UserCourseService> _logger = logger;

    public async Task<ResponseResult> AddUserCourseAsync(string userId, int courseId)
    {
        try
        {
            var userCourseEntity = new UserCourseEntity
            {
                UserId = userId,
                CourseId = courseId
            };

            return await _userCourseRepository.CreateOneAsync(userCourseEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error adding course for user {userId}: {ex.Message}");
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }

    public async Task<ResponseResult> GetUserCoursesAsync(string userId)
    {
        try
        {
            var result = await _userCourseRepository.GetAllAsync(uc => uc.UserId == userId);
            if (result.Status != StatusCode.OK)
            {
                return new ResponseResult { Status = result.Status, Message = result.Message };
            }

            var userCourses = (IEnumerable<UserCourseEntity>)result.ContentResult!;
            var userCourseDtos = userCourses.Select(entity => UserCourseFactory.Create(entity)).ToList();

            return new ResponseResult { Status = StatusCode.OK, ContentResult = userCourseDtos };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error fetching courses for user {userId}: {ex.Message}");
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }

    public async Task<ResponseResult> RemoveAllUserCoursesAsync(string userId)
    {
        try
        {
            return await _userCourseRepository.DeleteAllAsync(uc => uc.UserId == userId);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error removing all courses for user {userId}: {ex.Message}");
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }

    public async Task<ResponseResult> RemoveUserCourseAsync(string userId, int courseId)
    {
        try
        {
            return await _userCourseRepository.DeleteOneAsync(uc => uc.UserId == userId && uc.CourseId == courseId);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error removing course for user {userId}: {ex.Message}");
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }
}
