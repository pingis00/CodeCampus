using CodeCampus.Infrastructure.Contexts;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Interfaces.Repositories;
using CodeCampus.Infrastructure.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CodeCampus.Infrastructure.Repositories;

public class UserCourseRepository(DataContext context, ILogger<UserCourseRepository> logger) : Base<UserCourseEntity>(context, logger), IUserCourseRepository
{
    public async Task<ResponseResult> DeleteAllAsync(Expression<Func<UserCourseEntity, bool>> predicate)
    {
        try
        {
            var userCourses = await _context.UserCourses.Where(predicate).ToListAsync();

            if (userCourses.Count == 0)
            {
                return ResponseFactory.NotFound();
            }

            _context.UserCourses.RemoveRange(userCourses);
            await _context.SaveChangesAsync();

            return ResponseFactory.Ok("All courses successfully removed for the user.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting all user courses");
            return ResponseFactory.Error(ex.Message);
        }
    }

    public override async Task<ResponseResult> GetAllAsync(Expression<Func<UserCourseEntity, bool>> predicate)
    {
        try
        {
            var userCourses = await _context.UserCourses
            .Include(uc => uc.Course)
            .ThenInclude(c => c.Category)
            .Where(predicate)
            .ToListAsync();

            return ResponseFactory.Ok(userCourses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all user courses");
            return ResponseFactory.Error(ex.Message);
        }
    }
}
