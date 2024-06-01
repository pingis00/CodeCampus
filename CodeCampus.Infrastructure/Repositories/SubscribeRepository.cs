using CodeCampus.Infrastructure.Contexts;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Interfaces.Repositories;
using CodeCampus.Infrastructure.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeCampus.Infrastructure.Repositories;

public class SubscribeRepository(DataContext context, ILogger<SubscribeRepository> logger) : Base<SubscribeEntity>(context, logger), ISubscribeRepository
{
    public async Task<ResponseResult> IsEmailSubscribedAsync(string email)
    {
        try
        {
            var isSubscribed = await _context.Subscribers.AnyAsync(x => x.Email == email);
            return ResponseFactory.Ok(isSubscribed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while checking if email is subscribed");
            return ResponseFactory.Error(ex.Message);
        }
    }
}
