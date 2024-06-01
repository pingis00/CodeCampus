using CodeCampus.Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeCampus_WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserCoursesController(IUserCourseService userCourseService, ILogger<CoursesController> logger) : ControllerBase
{
    private readonly IUserCourseService _userCourseService = userCourseService;
    private readonly ILogger<CoursesController> _logger = logger;

    [HttpPost("add")]
    public async Task<IActionResult> AddUserCourse(string userId, int courseId)
    {
        try
        {
            var result = await _userCourseService.AddUserCourseAsync(userId, courseId);
            return StatusCode((int)result.Status, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding the course.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserCourses(string userId)
    {
        try
        {
            var result = await _userCourseService.GetUserCoursesAsync(userId);
            if (result.Status != CodeCampus.Infrastructure.Responses.StatusCode.OK)
            {
                return StatusCode((int)result.Status, result.Message);
            }

            return Ok(result.ContentResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the user courses.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveUserCourse(string userId, int courseId)
    {
        try
        {
            var result = await _userCourseService.RemoveUserCourseAsync(userId, courseId);
            return StatusCode((int)result.Status, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the course");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpDelete("user/{userId}/remove-all")]
    public async Task<IActionResult> RemoveAllUserCourses(string userId)
    {
        try
        {
            var result = await _userCourseService.RemoveAllUserCoursesAsync(userId);
            return StatusCode((int)result.Status, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the courses");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }
}
