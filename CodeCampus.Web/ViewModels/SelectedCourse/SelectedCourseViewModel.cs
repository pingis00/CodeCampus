using CodeCampus.Web.Models.Components;

namespace CodeCampus.Web.ViewModels.SelectedCourse;

public class SelectedCourseViewModel
{
    public CourseIntroViewModel CourseIntro = new()
    {
        Id = "Course-intro",
        BackgroundImage = new() { ImageUrl = "/Assets/Images/SelectedCourse/course-intro-background.svg", AltText = "background image" },
        HomeLink = new LinkComponent { ControllerName = "Home", ActionName = "Index", Url = "Home" },
        CoursesLink = new LinkComponent { ControllerName = "AvailableCourses", ActionName = "Index", Url = "availablecourses" },
        CourseView = "Fullstack Web Developer Course from Scratch",
        Tags = ["Best Seller", "Digital"],
        CourseTitle = "Fullstack Web Developer Course from Scratch",
        StarRating = new StarRatingComponent
        {
            StarIcon = new ImageComponent { ImageUrl = "/Assets/Icons/ui/star-icon.svg", AltText = "Star" },
            EmptyStarIcon = new ImageComponent { ImageUrl = "/Assets/Icons/ui/empty-star-icon.svg", AltText = "Empty Star" },
            NumberOfStars = 4,
            TotalStars = 5
        },
        Reviews = "(1.2K reviews)",
        Likes = "5K likes",
        CourseHours = "148 hours",
        CourseDescription = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
        AuthorImage = new ImageComponent { ImageUrl = "/Assets/Images/SelectedCourse/author-image-small.svg", AltText = "Image of the author albert flores" },
        AuthorText = "Created by",
        AuthorName = "Albert Flores"
    };

    public CourseDetailsViewModel CourseDetails = new()
    {
        Title = "Course Description",
        Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
        LearningOutcomes =
        [
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "/Assets/Icons/ui/bx-check-circle.svg", AltText = ""},
                Description = "Sed lectus donec amet eu turpis interdum."
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "/Assets/Icons/ui/bx-check-circle.svg", AltText = ""},
                Description = "Nulla at consectetur vitae dignissim porttitor."
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "/Assets/Icons/ui/bx-check-circle.svg", AltText = ""},
                Description = "Phasellus id vitae dui aliquet mi."
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "/Assets/Icons/ui/bx-check-circle.svg", AltText = ""},
                Description = "Integer cursus vitae, odio feugiat iaculis aliquet diam, et purus."
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "/Assets/Icons/ui/bx-check-circle.svg", AltText = ""},
                Description = "In aenean dolor diam tortor orci eu."
            },
        ],

        Includes = new CourseIncludes
        {
            Title = "This course includes:",
            OriginalPrice = "$49.00",
            SalePrice = "$28.99",
            JoinCourseButton = new LinkComponent
            {
                Text = "Join course"
            },
            Icon =
            [
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "/Assets/Images/SelectedCourse/video-icon.svg", AltText = "" },
                Description = "148 hours on-demand video"
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "/Assets/Images/SelectedCourse/file-icon.svg", AltText = "" },
                Description = "18 articles"
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "/Assets/Images/SelectedCourse/download-icon.svg", AltText = "" },
                Description = "25 downloadable resources"
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "/Assets/Images/SelectedCourse/infinite-icon.svg", AltText = "" },
                Description = "Full lifetime access"
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "/Assets/Images/SelectedCourse/trophy-icon.svg", AltText = "" },
                Description = "Certificate of completion"
            }
            ]
        },

        ProgramDetails =
        [
            new ProgramDetail { Number = "1", Title = "Introduction. Getting started", Description = "Nulla faucibus mauris pellentesque blandit faucibus non. Sit ut et at suspendisse gravida hendrerit tempus placerat."},
            new ProgramDetail { Number = "2", Title = "The ultimate HTML developer: advanced HTML", Description = "Lobortis diam elit id nibh ultrices sed penatibus donec. Nibh iaculis eu sit cras ultricies. Nam eu eget etiam egestas donec scelerisque ut ac enim. Vitae ac nisl, enim nec accumsan vitae est."},
            new ProgramDetail { Number = "3", Title = "CSS & CSS3: basic", Description = "Duis euismod enim, facilisis risus tellus pharetra lectus diam neque. Nec ultrices mi faucibus est. Magna ullamcorper potenti elementum ultricies auctor nec volutpat augue."},
            new ProgramDetail { Number = "4", Title = "JavaScript basics for beginners", Description = "Morbi porttitor risus imperdiet a, nisl mattis. Amet, faucibus eget in platea vitae, velit, erat eget velit. At lacus ut proin erat."},
            new ProgramDetail { Number = "5", Title = "Understanding APIs", Description = "Risus morbi euismod in congue scelerisque fusce pellentesque diam consequat. Nisi mauris nibh sed est morbi amet arcu urna. Malesuada feugiat quisque consectetur elementum diam vitae. Dictumst facilisis odio eu quis maecenas risus odio fames bibendum."},
            new ProgramDetail { Number = "6", Title = "C# and .NET from beginner to advanced", Description = "Quis risus quisque diam diam. Volutpat neque eget eu faucibus sed urna fermentum risus. Est, mauris morbi nibh massa."}
        ]
    };

    public InstructorInfoViewModel InstructorInfo = new()
    {
        InstructorImage = new ImageComponent { ImageUrl = "/Assets/Images/SelectedCourse/author-image-big.svg", AltText = "Image of instructor Albert Flores" },
        Title = "Learn from the best",
        InstructorName = "Albert Flores",
        Biography = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
        YoutubeLink = new LinkComponent
        {
            Url = "https://www.youtube.com",
            Text = "240k subscribers",
            Icon = new ImageComponent { ImageUrl = "/Assets/Icons/social/youtube-icon.svg", AltText = "Youtube Icon" }
        },
        FacebookLink = new LinkComponent
        {
            Url = "https://www.facebook.com",
            Text = "180k followers",
            Icon = new ImageComponent { ImageUrl = "/Assets/Icons/social/facebook-icon.svg", AltText = "Facebook Icon" }
        },
    };
}

