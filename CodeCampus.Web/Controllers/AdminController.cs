using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Interfaces.Services.Admin;
using CodeCampus.Web.Helpers;
using CodeCampus.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResponseStatusCode = CodeCampus.Infrastructure.Responses.StatusCode;

namespace CodeCampus.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(IAdminCourseService adminCourseService, IAdminContactService adminContactService, IAdminSubscribeService adminSubscriberService, ILogger<AdminController> logger) : Controller
{
    private readonly IAdminCourseService _adminCourseService = adminCourseService;
    private readonly IAdminContactService _adminContactService = adminContactService;
    private readonly IAdminSubscribeService _adminSubscriberService = adminSubscriberService;
    private readonly ILogger<AdminController> _logger = logger;

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult AddCourse()
    {
        var viewModel = new CourseCreateViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddCourse(CourseCreateViewModel viewModel, IFormFile? courseImageFile)
    {
        if (!ModelState.IsValid)
        {
            TempData["Message"] = "Please correct the errors in the form.";
            TempData["MessageType"] = "error";
            return View(viewModel);
        }

        try
        {
            var courseDto = AdminMappingFactory.MapToCreateDto(viewModel);

            var result = await _adminCourseService.AddAdminCourseAsync(courseDto, courseImageFile);

            if (result.Status == ResponseStatusCode.OK)
            {
                TempData["Message"] = "Course added successfully.";
                TempData["MessageType"] = "success";
                return RedirectToAction(nameof(Courses));
            }
            else if (result.Status == ResponseStatusCode.UNAUTHORIZED)
            {
                TempData["Message"] = "Unauthorized access. Please check your API key.";
                TempData["MessageType"] = "error";
            }
            else
            {
                TempData["Message"] = "Failed to add course. Please try again.";
                TempData["MessageType"] = "error";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding course.");
            TempData["Message"] = "An error occurred while adding the course.";
            TempData["MessageType"] = "error";
        }

        return View(viewModel);
    }

    public async Task<IActionResult> Courses()
    {
        try
        {
            var result = await _adminCourseService.GetAllAdminCoursesAsync();

            if (result.Status != ResponseStatusCode.OK)
            {
                TempData["Message"] = result.Message ?? "Error fetching courses from API.";
                TempData["MessageType"] = "error";
                return View("Error");
            }

            if (result.ContentResult is not List<CourseGetRequestDto> courseDtos)
            {
                TempData["Message"] = "No courses found.";
                TempData["MessageType"] = "error";
                return View(new List<CourseCreateViewModel>());
            }

            var courseViewModels = courseDtos.Select(AdminMappingFactory.MapToCreateViewModel).ToList();
            return View(courseViewModels);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching courses.");
            TempData["Message"] = "An error occurred while fetching the courses.";
            TempData["MessageType"] = "error";
            return View("Error");
        }
    }

    public async Task<IActionResult> CourseDetails(int id)
    {
        try
        {
            var result = await _adminCourseService.GetOneAdminCourseAsync(id);

            if (result.Status != ResponseStatusCode.OK)
            {
                TempData["Message"] = result.Message ?? "Error fetching course details from API.";
                TempData["MessageType"] = "error";
                return View("Error");
            }

            var course = result.ContentResult as CourseGetRequestDto;
            var courseViewModel = AdminMappingFactory.MapToCreateViewModel(course!);
            return View(courseViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching course details.");
            TempData["Message"] = "An error occurred while fetching the course details.";
            TempData["MessageType"] = "error";
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> UpdateCourse(int id)
    {
        try
        {
            var result = await _adminCourseService.GetOneAdminCourseAsync(id);

            if (result.Status != ResponseStatusCode.OK)
            {
                TempData["Message"] = result.Message ?? "Error fetching course details from API.";
                TempData["MessageType"] = "error";
                return View("Error");
            }

            if (result.ContentResult is not CourseGetRequestDto courseDto)
            {
                TempData["Message"] = "Course not found.";
                TempData["MessageType"] = "error";
                return View("Error");
            }

            var courseViewModel = AdminMappingFactory.MapToUpdateViewModel(courseDto);
            return View(courseViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching course details for update.");
            TempData["Message"] = "An error occurred while fetching the course details.";
            TempData["MessageType"] = "error";
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCourse(CourseUpdateViewModel viewModel, IFormFile? courseImageFile)
    {
        if (!ModelState.IsValid)
        {
            TempData["Message"] = "Please correct the errors in the form.";
            TempData["MessageType"] = "error";
            return View(viewModel);
        }

        try
        {
            var courseDto = AdminMappingFactory.MapToUpdateDto(viewModel);
            var result = await _adminCourseService.UpdateAdminCourseAsync(viewModel.Id, courseDto, courseImageFile);

            if (result.Status == ResponseStatusCode.OK)
            {
                TempData["Message"] = "Please correct the errors in the form.";
                TempData["MessageType"] = "success";
                return RedirectToAction(nameof(CourseDetails), new { id = viewModel.Id });
            }
            else
            {
                TempData["Message"] = "Failed to update course. Please try again.";
                TempData["MessageType"] = "error";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating course.");
            TempData["Message"] = "An error occurred while updating the course.";
            TempData["MessageType"] = "error";
        }

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        try
        {
            var result = await _adminCourseService.DeleteAdminCourseAsync(id);

            if (result.Status == ResponseStatusCode.OK)
            {
                TempData["Message"] = "Course deleted successfully.";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "Failed to delete course. Please try again.";
                TempData["MessageType"] = "error";
            }

            return RedirectToAction(nameof(Courses));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting course.");
            TempData["Message"] = "An error occurred while deleting the course.";
            TempData["MessageType"] = "error";
            return RedirectToAction(nameof(Courses));
        }
    }

    public async Task<IActionResult> ContactRequests()
    {
        try
        {
            var result = await _adminContactService.GetAllAdminContactsAsync();

            if (result.Status == ResponseStatusCode.OK)
            {
                var contacts = result.ContentResult as List<ContactRequestDto>;
                var contactViewModels = contacts?.Select(AdminMappingFactory.MapToViewModel).ToList();
                return View(contactViewModels);
            }

            TempData["Message"] = "Error fetching contact requests.";
            TempData["MessageType"] = "error";
            return View(new List<ContactRequestViewModel>());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching contact requests.");
            TempData["Message"] = "An error occurred while fetching contact requests.";
            TempData["MessageType"] = "error";
            return View(new List<ContactRequestViewModel>());
        }
    }

    public async Task<IActionResult> ContactRequestDetails(int id)
    {
        try
        {
            var result = await _adminContactService.GetAdminContactByIdAsync(id);

            if (result.Status == ResponseStatusCode.OK)
            {
                var contact = result.ContentResult as ContactRequestDto;
                var contactViewModel = AdminMappingFactory.MapToViewModel(contact!);
                return View(contactViewModel);
            }

            TempData["Message"] = "Error fetching contact request details.";
            TempData["MessageType"] = "error";
            return View("Error");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching contact request details.");
            TempData["Message"] = "An error occurred while fetching contact request details.";
            TempData["MessageType"] = "error";
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteContactRequest(int id)
    {
        try
        {
            var result = await _adminContactService.DeleteAdminContactRequestAsync(id);

            if (result.Status == ResponseStatusCode.OK)
            {
                TempData["Message"] = "Contact request deleted successfully.";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "Failed to delete contact request. Please try again.";
                TempData["MessageType"] = "error";
            }

            return RedirectToAction(nameof(ContactRequests));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting contact request.");
            TempData["Message"] = "An error occurred while deleting the contact request.";
            TempData["MessageType"] = "error";
            return RedirectToAction(nameof(ContactRequests));
        }
    }

    public async Task<IActionResult> Subscribers()
    {
        try
        {
            var result = await _adminSubscriberService.GetAllAdminSubscribersAsync();

            if (result.Status != ResponseStatusCode.OK)
            {
                TempData["Message"] = result.Message ?? "Error fetching subscribers from API.";
                TempData["MessageType"] = "error";
                return View("Error");
            }

            var subscribers = result.ContentResult as List<SubscriberDto>;
            var subscriberViewModels = subscribers?.Select(AdminMappingFactory.MapToViewModel).ToList();
            return View(subscriberViewModels);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching subscriber details.");
            TempData["Message"] = "An error occurred while fetching subscriber details.";
            TempData["MessageType"] = "error";
            return View("Error");
        }
    }

    public async Task<IActionResult> SubscriberDetails(int id)
    {
        try
        {
            var result = await _adminSubscriberService.GetAdminSubscriberByIdAsync(id);

            if (result.Status != ResponseStatusCode.OK)
            {
                TempData["Message"] = result.Message ?? "Error fetching subscriber details from API.";
                TempData["MessageType"] = "error";
                return View("Error");
            }

            var subscriber = result.ContentResult as SubscriberDto;
            var subscriberViewModel = AdminMappingFactory.MapToViewModel(subscriber!);
            return View(subscriberViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching subscriber details.");
            TempData["Message"] = "An error occurred while fetching subscriber details.";
            TempData["MessageType"] = "error";
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteSubscriber(int id)
    {
        try
        {
            var result = await _adminSubscriberService.DeleteAdminSubscriberAsync(id);

            if (result.Status == ResponseStatusCode.OK)
            {
                TempData["Message"] = "Subscriber deleted successfully.";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "Failed to delete subscriber. Please try again.";
                TempData["MessageType"] = "error";
            }

            return RedirectToAction(nameof(Subscribers));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting subscriber.");
            TempData["Message"] = "An error occurred while deleting the subscriber.";
            TempData["MessageType"] = "error";
            return RedirectToAction(nameof(Subscribers));
        }
    }
}
