using CodeCampus.Infrastructure.Contexts;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Interfaces.Repositories;
using CodeCampus.Infrastructure.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;

namespace CodeCampus.Infrastructure.Repositories;

public class CourseRepository(DataContext context, ILogger<CourseRepository> logger) : Base<CourseEntity>(context, logger), ICourseRepository
{
    public override async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            var courses = await _context.Courses
                .Include(c => c.Category)
                .ToListAsync();

            return ResponseFactory.Ok(courses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all courses");
            return ResponseFactory.Error(ex.Message);
        }
    }

    public override async Task<ResponseResult> GetOneAsync(Expression<Func<CourseEntity, bool>> predicate)
    {
        try
        {
            var course = await _context.Courses
                .Include(c => c.Category)
                .FirstOrDefaultAsync(predicate);

            if (course == null)
                return ResponseFactory.NotFound();

            return ResponseFactory.Ok(course);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting a course");
            return ResponseFactory.Error(ex.Message);
        }
    }
}

