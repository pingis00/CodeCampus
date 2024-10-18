using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Helpers;
using CodeCampus.Web.Helpers;
using CodeCampus.Web.ViewModels.SelectedCourse;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CodeCampus.Web.Controllers;

public class SelectedCourseController(HttpClientHelper httpClientHelper, UserManager<UserEntity> userManager, ILogger<SelectedCourseController> logger) : Controller
{
    private readonly HttpClientHelper _httpClientHelper = httpClientHelper;
    private readonly UserManager<UserEntity> _usermanager = userManager;
    private readonly ILogger<SelectedCourseController> _logger = logger;

    [Route("/selectedcourse/{id}")]
    public async Task<IActionResult> Index(int id)
    {
        try
        {
            var httpClient = _httpClientHelper.CreateHttpClientWithApiKey();

            var response = await httpClient.GetAsync($"https://localhost:7297/api/courses/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Error fetching course details from API.";
                TempData["MessageType"] = "error";
                _logger.LogError("Error fetching course details from API. Status code: {StatusCode}", response.StatusCode);
                return View("Error");
            }

            var courseDto = await response.Content.ReadFromJsonAsync<CourseGetRequestDto>();
            if (courseDto == null)
            {
                TempData["Message"] = "Error fetching course details from API.";
                TempData["MessageType"] = "error";
                _logger.LogError("Error fetching course details from API. No course returned.");
                return View("Error");
            }

            var viewModel = new SelectedCourseViewModel
            {
                CourseIntro = AdminMappingFactory.MapToCourseIntroViewModel(courseDto),
                CourseDetails = AdminMappingFactory.MapToCourseDetailsViewModel(courseDto),
                InstructorInfo = AdminMappingFactory.MapToInstructorInfoViewModel(courseDto)
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while fetching course details.");
            TempData["Message"] = "An unexpected error occurred. Please try again later.";
            TempData["MessageType"] = "error";
            return View("Error");
        }
    }

    [HttpPost]
    [Route("/selectedcourse/join-course")]
    public async Task<IActionResult> JoinCourse(int courseId)
    {
        try
        {
            var user = await _usermanager.Users
                .Include(u => u.UserCourses)
                .FirstOrDefaultAsync(u => u.Id == _usermanager.GetUserId(User));
            if (user == null)
            {
                TempData["Message"] = "User not found.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index", "SelectedCourse", new { id = courseId });
            }

            var httpClient = _httpClientHelper.CreateHttpClientWithApiKey();

            var response = await httpClient.GetAsync($"https://localhost:7297/api/courses/{courseId}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Course not found.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index", "SelectedCourse", new { id = courseId });
            }

            var courseJson = await response.Content.ReadAsStringAsync();
            var courseDto = JsonSerializer.Deserialize<CourseGetRequestDto>(courseJson);

            if (courseDto == null)
            {
                TempData["Message"] = "Course not found.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index", "SelectedCourse", new { id = courseId });
            }

            if (user.UserCourses.Any(uc => uc.CourseId == courseId))
            {
                TempData["Message"] = "Course is already added.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index", "SelectedCourse", new { id = courseId });
            }

            var joinResponse = await httpClient.PostAsync($"https://localhost:7297/api/usercourses/add?userId={user.Id}&courseId={courseId}", null);

            if (!joinResponse.IsSuccessStatusCode)
            {
                TempData["Message"] = "Error joining the course.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index", "SelectedCourse", new { id = courseId });
            }

            TempData["Message"] = "Course added successfully.";
            TempData["MessageType"] = "success";
            return RedirectToAction("SavedCourses", "Account");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while joining the course.");
            TempData["Message"] = "An unexpected error occurred. Please try again later.";
            TempData["MessageType"] = "error";
            return RedirectToAction("Index", new { id = courseId });
        }
    }
}
