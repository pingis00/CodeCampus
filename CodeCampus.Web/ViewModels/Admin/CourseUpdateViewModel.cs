using CodeCampus.Infrastructure.DTOs;

namespace CodeCampus.Web.ViewModels.Admin;

public class CourseUpdateViewModel
{
    public int Id { get; set; }
    public CourseUpdateRequestDto CourseUpdate { get; set; } = new CourseUpdateRequestDto();
    public int CategoryId { get; set; }
}
