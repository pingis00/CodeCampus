using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ResponseStatusCode = CodeCampus.Infrastructure.Responses.StatusCode;

namespace CodeCampus_WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscribeController(ILogger<SubscribeController> logger, ISubscribeService subscribeService) : ControllerBase
{
    private readonly ISubscribeService _subscribeService = subscribeService;
    private readonly ILogger<SubscribeController> _logger = logger;

    [HttpPost]
    public async Task<IActionResult> SubscribeEmail([FromBody] SubscribeRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _subscribeService.IsEmailSubscribedAsync(request.Email);
            if (result.Status == ResponseStatusCode.EXISTS)
            {
                return Conflict(result.Message);
            }

            var userId = User?.Identity?.IsAuthenticated == true ? User.FindFirstValue(ClaimTypes.NameIdentifier) : null;
            await _subscribeService.SubscribeEmailAsync(request, userId);
            return CreatedAtAction(nameof(SubscribeEmail), new { email = request.Email }, "Successfully subscribed.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while subscribing email: {Email}", request.Email);
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSubscribers()
    {
        try
        {
            var result = await _subscribeService.GetAllSubscribersAsync();
            if (result.Status == ResponseStatusCode.OK)
            {
                return Ok(result.ContentResult);
            }
            return StatusCode((int)result.Status, result.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving subscribers.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubscriberById(int id)
    {
        try
        {
            var result = await _subscribeService.GetSubscriberByIdAsync(id);
            if (result.Status == ResponseStatusCode.OK)
            {
                return Ok(result.ContentResult);
            }
            return StatusCode((int)result.Status, result.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the subscriber.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> UnSubscribeEmail(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _subscribeService.UnsubscribeEmailAsync(id);
            if (result.Status == ResponseStatusCode.NOT_FOUND)
            {
                return NotFound(result.Message);
            }
            if (result.Status == ResponseStatusCode.OK)
            {
                return Ok("Successfully unsubscribed.");
            }
            return StatusCode((int)result.Status, result.Message);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while unsubscribing email.");
            return StatusCode(500, "Internal server error. Please try again later.");
        }

    }
}
