using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Responses;
using Microsoft.AspNetCore.Http;

namespace CodeCampus.Infrastructure.Interfaces.Services.Admin;

public interface IAdminCourseService
{
    Task<ResponseResult> AddAdminCourseAsync(CourseRequestDto courseDto, IFormFile? courseImageFile);
    Task<ResponseResult> UpdateAdminCourseAsync(int id, CourseUpdateRequestDto courseDto, IFormFile? courseImageFile);
    Task<ResponseResult> DeleteAdminCourseAsync(int id);
    Task<ResponseResult> GetAllAdminCoursesAsync();
    Task<ResponseResult> GetOneAdminCourseAsync(int id);
}
