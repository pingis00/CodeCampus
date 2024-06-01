using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using ResponseStatusCode = CodeCampus.Infrastructure.Responses.StatusCode;

namespace CodeCampus_WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController(ICourseService courseService, ILogger<CoursesController> logger, IFileUploadService fileUploader) : ControllerBase
{
    private readonly ICourseService _courseService = courseService;
    private readonly IFileUploadService _fileUploader = fileUploader;
    private readonly ILogger<CoursesController> _logger = logger;

    [HttpPost("admin")]
    public async Task<IActionResult> CreateCourseAdmin([FromForm] CourseRequestDto request, IFormFile? courseImageFile)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            if (courseImageFile != null)
            {
                request.CourseImage = await _fileUploader.UploadFileAsync(courseImageFile, "courseimages");
            }

            var courseModel = CourseFactory.Create(request);
            var result = await _courseService.CreateCourseAsync(courseModel);
            if (result.Status == ResponseStatusCode.EXISTS)
            {
                return Conflict(result.Message);
            }

            return CreatedAtAction(nameof(GetOneCourse), new { id = ((CourseEntity)result.ContentResult!).Id }, result.ContentResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the course.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpGet("admin/{id}")]
    public async Task<IActionResult> GetOneCourseAdmin(int id)
    {
        try
        {
            var result = await _courseService.GetCourseByIdAsync(id);
            if (result.Status == ResponseStatusCode.OK)
            {
                return Ok(result.ContentResult);
            }
            return StatusCode((int)result.Status, result.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the course.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpGet("admin")]
    public async Task<IActionResult> GetAllCoursesAdmin()
    {
        try
        {
            var result = await _courseService.GetAllCoursesAsync();
            if (result.Status == ResponseStatusCode.OK)
            {
                return Ok(result.ContentResult);
            }
            return StatusCode((int)result.Status, result.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving courses.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpPut("admin/{id}")]
    public async Task<IActionResult> UpdateCourseAdmin(int id, [FromForm] CourseUpdateRequestDto request, IFormFile? courseImageFile)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            if (courseImageFile != null)
            {
                request.CourseImage = await _fileUploader.UploadFileAsync(courseImageFile, "courseimages");
            }

            var courseModel = CourseFactory.Create(request);
            var result = await _courseService.UpdateCourseAsync(id, courseModel);
            if (result.Status == ResponseStatusCode.NOT_FOUND)
            {
                return NotFound(result.Message);
            }
            if (result.Status == ResponseStatusCode.EXISTS)
            {
                return Conflict(result.Message);
            }
            return Ok(result.ContentResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the course.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpDelete("admin/{id}")]
    public async Task<IActionResult> DeleteCourseAdmin(int id)
    {
        try
        {
            var result = await _courseService.DeleteCourseAsync(id);
            if (result.Status == ResponseStatusCode.NOT_FOUND)
            {
                return NotFound(result.Message);
            }
            return Ok("Successfully deleted the course.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the course.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneCourse(int id)
    {
        try
        {
            var result = await _courseService.GetCourseByIdAsync(id);
            if (result.Status == ResponseStatusCode.OK)
            {
                return Ok(result.ContentResult);
            }
            return StatusCode((int)result.Status, result.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the course.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }
}
