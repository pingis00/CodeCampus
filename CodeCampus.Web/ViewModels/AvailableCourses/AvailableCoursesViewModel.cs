using CodeCampus.Web.Models.Components;
using CodeCampus.Web.ViewModels.Account;

namespace CodeCampus.Web.ViewModels.AvailableCourses;

public class AvailableCoursesViewModel
{
    public string Title { get; set; } = "Available Courses";

    public CourseSearchViewModel CourseSearch = new()
    {
        Id = "CourseSearch",
        HomeLink = "Home",
        CoursesText = "Courses",
        Title = "Courses",
        Categories = []
    };

    public SavedCoursesViewModel Courses { get; set; } = new SavedCoursesViewModel();
    public string? ErrorMessage { get; set; }

    public GetStartedViewModel GetStarted = new()
    {
        Id = "get-started",
        PreHighlightTitle = "Take Your",
        HighlightedWord = "Skills",
        PostHighlightTitle = "to the Next Level",
        SubTitle = "Ready to get started?",
        CtaButton = new LinkComponent
        {
            ControllerName = "Career",
            ActionName = "WorkWithUs",
            Text = "Work with us",
            Url = "/workwithus"
        },
        Image = new ImageComponent
        {
            ImageUrl = "/Assets/Images/Homepage/illustration-image-light.svg",
            DarkModeImageUrl = "/Assets/Images/Homepage/illustration-image-dark.svg",
            AltText = "Illustration"
        }
    };

}
