using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Interfaces.Services;
using CodeCampus_WebApi.Attrubutes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using ResponseStatusCode = CodeCampus.Infrastructure.Responses.StatusCode;

namespace CodeCampus_WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController(IContactService contactService, ILogger<ContactController> logger, UserManager<UserEntity> userManager) : ControllerBase
{
    private readonly IContactService _contactService = contactService;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly ILogger<ContactController> _logger = logger;

    [HttpPost]
    [ApiKey(requireAdmin: false)]
    public async Task<IActionResult> ContactRequest([FromBody] ContactRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _contactService.SubmitContactRequestAsync(request, null);
            return CreatedAtAction(nameof(ContactRequest), new { email = request.Email }, "Contact request submitted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while submitting contact request.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpGet]
    [ApiKey(requireAdmin: true)]
    public async Task<IActionResult> GetAllContacts()
    {
        try
        {
            var result = await _contactService.GetAllContactsAsync();
            if (result.Status == ResponseStatusCode.OK)
            {
                return Ok(result.ContentResult);
            }
            return StatusCode((int)result.Status, result.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving contacts.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpGet("{id}")]
    [ApiKey(requireAdmin: true)]
    public async Task<IActionResult> GetContactById(int id)
    {
        try
        {
            var result = await _contactService.GetContactByIdAsync(id);
            if (result.Status == ResponseStatusCode.OK)
            {
                return Ok(result.ContentResult);
            }
            return StatusCode((int)result.Status, result.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the contact.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpDelete("{id}")]
    [ApiKey(requireAdmin: true)]
    public async Task<IActionResult> DeleteContactRequest(int id)
    {
        try
        {
            var result = await _contactService.DeleteContactRequestAsync(id);
            if (result.Status == ResponseStatusCode.OK)
            {
                return Ok("Contact request deleted successfully.");
            }
            return StatusCode((int)result.Status, result.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the contact request.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }
}
