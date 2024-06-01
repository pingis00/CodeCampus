using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Interfaces.Repositories;
using CodeCampus.Infrastructure.Interfaces.Services;
using CodeCampus.Infrastructure.Models;
using CodeCampus.Infrastructure.Responses;
using Microsoft.Extensions.Logging;

namespace CodeCampus.Infrastructure.Services;

public class CourseService(ICourseRepository courseRepository, ICategoryService categoryService, ILogger<CourseService> logger) : ICourseService
{
    private readonly ICourseRepository _courseRepository = courseRepository;
    private readonly ICategoryService _categoryService = categoryService;
    private readonly ILogger<CourseService> _logger = logger;


    public async Task<ResponseResult> CreateCourseAsync(Course model)
    {
        try
        {
            var existsResult = await _courseRepository.AlreadyExistsAsync(c => c.Title == model.Title && c.Author == model.Author);
            if (existsResult.Status == StatusCode.EXISTS)
            {
                return new ResponseResult { Status = StatusCode.EXISTS, Message = "A course with the same title and author already exists." };
            }

            var categoryResult = await _categoryService.GetOrCreateCategoryAsync(model.Category);
            if (categoryResult.Status != StatusCode.OK)
            {
                return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = "Failed to get or create category." };
            }

            var category = (CategoryEntity)categoryResult.ContentResult!;
            var courseEntity = CourseFactory.Create(model, category.Id);

            return await _courseRepository.CreateOneAsync(courseEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating course");
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }

    public async Task<ResponseResult> GetCourseByIdAsync(int id)
    {
        try
        {
            var result = await _courseRepository.GetOneAsync(c => c.Id == id);
            if (result.Status != StatusCode.OK)
            {
                return new ResponseResult { Status = StatusCode.NOT_FOUND, Message = "Course not found." };
            }
            var courseEntity = (CourseEntity)result.ContentResult!;
            var courseModel = CourseFactory.Create(courseEntity);

            return new ResponseResult { Status = StatusCode.OK, ContentResult = courseModel };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving course by id");
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }

    public async Task<ResponseResult> GetAllCoursesAsync()
    {
        try
        {
            var result = await _courseRepository.GetAllAsync();
            if (result.Status != StatusCode.OK)
            {
                return new ResponseResult { Status = result.Status, Message = result.Message };
            }
            var courseEntities = (IEnumerable<CourseEntity>)result.ContentResult!;
            var courseModels = courseEntities.Select(entity => CourseFactory.Create(entity)).ToList();

            return new ResponseResult { Status = StatusCode.OK, ContentResult = courseModels };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all courses");
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }

    public async Task<ResponseResult> UpdateCourseAsync(int id, Course model)
    {
        try
        {
            var existingCourseResult = await _courseRepository.GetOneAsync(c => c.Id == id);
            if (existingCourseResult.Status != StatusCode.OK)
            {
                return new ResponseResult { Status = StatusCode.NOT_FOUND, Message = "Course not found." };
            }

            var conflictResult = await _courseRepository.AlreadyExistsAsync(c => c.Title == model.Title && c.Author == model.Author && c.Id != id);
            if (conflictResult.Status == StatusCode.EXISTS)
            {
                return new ResponseResult { Status = StatusCode.EXISTS, Message = "A course with the same title and author already exists." };
            }

            var categoryResult = await _categoryService.GetOrCreateCategoryAsync(model.Category);
            if (categoryResult.Status != StatusCode.OK && categoryResult.Status != StatusCode.OK)
            {
                return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = "Failed to create or retrieve category." };
            }

            var category = (CategoryEntity)categoryResult.ContentResult!;
            var existingCourse = (CourseEntity)existingCourseResult.ContentResult!;
            existingCourse.Title = model.Title;
            existingCourse.Author = model.Author;
            existingCourse.Price = model.Price;
            existingCourse.DiscountPrice = model.DiscountPrice;
            existingCourse.Hours = model.Hours;
            existingCourse.LikesInProcent = model.LikesInProcent;
            existingCourse.LikesInNumbers = model.LikesInNumbers;
            existingCourse.IsBestSeller = model.IsBestSeller;
            existingCourse.CategoryId = category.Id;

            if (!string.IsNullOrEmpty(model.CourseImage))
            {
                existingCourse.CourseImage = model.CourseImage;
            }

            return await _courseRepository.UpdateOneAsync(c => c.Id == id, existingCourse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating course");
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }

    public async Task<ResponseResult> DeleteCourseAsync(int id)
    {
        try
        {
            var result = await _courseRepository.GetOneAsync(c => c.Id == id);
            if (result.Status != StatusCode.OK)
            {
                return new ResponseResult { Status = StatusCode.NOT_FOUND, Message = "Course not found." };
            }
            return await _courseRepository.DeleteOneAsync(c => c.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting course");
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }
}
