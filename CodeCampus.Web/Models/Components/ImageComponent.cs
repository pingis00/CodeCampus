using System.ComponentModel.DataAnnotations;

namespace CodeCampus.Web.Models.Components;

public class ImageComponent
{
    [DataType(DataType.ImageUrl)]
    public string? ImageUrl { get; set; }
    public string? AltText { get; set; }
    public string? DarkModeImageUrl { get; set; }
}
