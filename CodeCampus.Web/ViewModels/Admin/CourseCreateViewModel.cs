using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Web.Models.Components;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeCampus.Web.ViewModels.Admin;

public class CourseCreateViewModel
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public CourseRequestDto CourseRequest { get; set; } = new CourseRequestDto();
    public List<CourseComponent> Courses { get; set; } = [];

    public IEnumerable<SelectListItem> YesOrNo { get; set; } =
    [
        new SelectListItem { Value = "true", Text = "Yes" },
        new SelectListItem { Value = "false", Text = "No" }
    ];

}
