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
        SelectedCategory = "",
        SearchQuery = "",
        Categories =
        [
            "All categories",
            "Technology",
            "Business",
            "Art & Design",
            "Science"
        ]
    };

    public SavedCoursesViewModel Courses = new()
    {


        Title = "Saved Items",
        Courses =
                [
                    new CourseComponent
                    {
                        Id = 1,
                        CourseImage = new ImageComponent { ImageUrl = "/Assets/Images/Courses/courseimage1.svg", AltText = "Inledning till programmering" },
                        Title = "Fullstack Web Developer Course from Scratch",
                        CourseAuthor = "By Robert Fox",
                        CoursePrice = "$12.50",
                        CourseHours = "220 hours",
                        Likes = "94% (4.2K)"
                    },
                    new CourseComponent
                    {
                        Id = 2,
                        CourseImage = new ImageComponent { ImageUrl = "/Assets/Images/Courses/courseimage2.svg", AltText = "Inledning till programmering" },
                        Title = "HTML, CSS, JavaScript Web Developer",
                        CourseAuthor = "By Jenny Wilson & Marvin McKinney",
                        CoursePrice = "$15.99",
                        CourseHours = "160 hours",
                        Likes = "92% (3.1k)"
                    },
                    new CourseComponent
                    {
                        Id = 3,
                        CourseImage = new ImageComponent { ImageUrl = "/Assets/Images/Courses/courseimage3.svg", AltText = "Inledning till programmering" },
                        Title = "The Complete Front-End Web Development Course",
                        CourseAuthor = "By Albert Flores",
                        CoursePrice = "$9.99",
                        CourseHours = "100 hours",
                        Likes = "98% (2.7K)"
                    },
                    new CourseComponent
                    {
                        Id = 4,
                        CourseImage = new ImageComponent { ImageUrl = "/Assets/Images/Courses/courseimage4.svg", AltText = "Inledning till programmering" },
                        Title = "iOS & Swift - The Complete iOS App Development Course",
                        CourseAuthor = "By Marvin McKinney",
                        CoursePrice = "$15.99",
                        CourseHours = "160 hours",
                        Likes = "92% (3.1k)"
                    },
                    new CourseComponent
                    {
                        Id = 5,
                        CourseImage = new ImageComponent { ImageUrl = "/Assets/Images/Courses/courseimage5.svg", AltText = "Inledning till programmering" },
                        Title = "Data Science & Machine Learning with Python",
                        CourseAuthor = "By Esther Howard",
                        CoursePrice = "$11.20",
                        CourseHours = "160 hours",
                        Likes = "92% (3.1k)"
                    },
                    new CourseComponent
                    {
                        Id = 6,
                        CourseImage = new ImageComponent { ImageUrl = "/Assets/Images/Courses/courseimage6.svg", AltText = "Inledning till programmering" },
                        Title = "Creative CSS Drawing Course: Make Art With CSS",
                        CourseAuthor = "By Robert Fox",
                        CoursePrice = "$10.50",
                        CourseHours = "220 hours",
                        Likes = "94% (4.2K)"
                    },
                    new CourseComponent
                    {
                        Id = 7,
                        CourseImage = new ImageComponent { ImageUrl = "/Assets/Images/Courses/courseimage7.svg", AltText = "Inledning till programmering" },
                        Title = "Blender Character Creator v2.0 for Video Games Design",
                        CourseAuthor = "By Ralph Edwards",
                        CoursePrice = "$18.99",
                        CourseHours = "160 hours",
                        Likes = "92% (3.1k)"
                    },
                    new CourseComponent
                    {
                        Id = 8,
                        CourseImage = new ImageComponent { ImageUrl = "/Assets/Images/Courses/courseimage8.svg", AltText = "Inledning till programmering" },
                        Title = "The Ultimate Guide to 2D Mobile Game Development with Unity",
                        CourseAuthor = "By Albert Flores",
                        CoursePrice = "$12.99",
                        CourseHours = "100 hours",
                        Likes = "98% (2.7k)"
                    },
                    new CourseComponent
                    {
                        Id = 9,
                        CourseImage = new ImageComponent { ImageUrl = "/Assets/Images/Courses/courseimage9.svg", AltText = "Inledning till programmering" },
                        Title = "Learn JMETER from Scratch on Live Apps-Performance Testing",
                        CourseAuthor = "By Jenny Wilson",
                        CoursePrice = "$14.50",
                        CourseHours = "160 hours",
                        Likes = "92% (3.1k)"
                    }
                ]
    };

    public GetStartedViewModel GetStarted = new()
    {
        Id = "get-started",
        PreHighlightTitle = "Take Your",
        HighlightedWord = "Skills",
        PostHighlightTitle = "to the Next Level",
        SubTitle = "Ready to get started?",
        CtaButton = new LinkComponent { ControllerName = "Career", ActionName = "WorkWithUs", Text = "Work with us", Url = "/workwithus" },
        Image = new ImageComponent { 
            ImageUrl = "/Assets/Images/Homepage/illustration-image-light.svg",
            DarkModeImageUrl = "/Assets/Images/Homepage/illustration-image-dark.svg",
            AltText = "Illustration" 
        
        }
    };
}
