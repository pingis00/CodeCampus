using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Web.Models.Components;
using CodeCampus.Web.ViewModels.AvailableCourses;
using CodeCampus.Web.ViewModels.SelectedCourse;

namespace CodeCampus.Web.Helpers;

public static class AdminMappingFactory
{
    //public static CourseCreateViewModel MapToCreateViewModel(CourseGetRequestDto dto)
    //{
    //    return new CourseCreateViewModel
    //    {
    //        Id = dto.Id,
    //        CategoryId = dto.CategoryId,
    //        CourseRequest = new CourseRequestDto
    //        {
    //            CourseImage = dto.CourseImage,
    //            Title = dto.Title,
    //            Author = dto.Author,
    //            Price = dto.Price,
    //            DiscountPrice = dto.DiscountPrice,
    //            Hours = dto.Hours,
    //            LikesInProcent = dto.LikesInProcent,
    //            LikesInNumbers = dto.LikesInNumbers,
    //            IsBestSeller = dto.IsBestSeller,
    //            CategoryName = dto.Category,
    //        }
    //    };
    //}

    //public static CourseRequestDto MapToCreateDto(CourseCreateViewModel viewModel)
    //{
    //    return new CourseRequestDto
    //    {
    //        CourseImage = viewModel.CourseRequest.CourseImage,
    //        Title = viewModel.CourseRequest.Title,
    //        Author = viewModel.CourseRequest.Author,
    //        Price = viewModel.CourseRequest.Price,
    //        DiscountPrice = viewModel.CourseRequest.DiscountPrice,
    //        Hours = viewModel.CourseRequest.Hours,
    //        LikesInProcent = viewModel.CourseRequest.LikesInProcent,
    //        LikesInNumbers = viewModel.CourseRequest.LikesInNumbers,
    //        IsBestSeller = viewModel.CourseRequest.IsBestSeller,
    //        CategoryName = viewModel.CourseRequest.CategoryName,
    //    };
    //}

    //public static CourseUpdateRequestDto MapToUpdateDto(CourseUpdateViewModel viewModel)
    //{
    //    return new CourseUpdateRequestDto
    //    {
    //        CourseImage = viewModel.CourseUpdate.CourseImage,
    //        Title = viewModel.CourseUpdate.Title,
    //        Author = viewModel.CourseUpdate.Author,
    //        Price = viewModel.CourseUpdate.Price,
    //        DiscountPrice = viewModel.CourseUpdate.DiscountPrice,
    //        Hours = viewModel.CourseUpdate.Hours,
    //        LikesInProcent = viewModel.CourseUpdate.LikesInProcent,
    //        LikesInNumbers = viewModel.CourseUpdate.LikesInNumbers,
    //        IsBestSeller = viewModel.CourseUpdate.IsBestSeller,
    //        CategoryName = viewModel.CourseUpdate.CategoryName
    //    };
    //}

    //public static CourseUpdateViewModel MapToUpdateViewModel(CourseGetRequestDto dto)
    //{
    //    return new CourseUpdateViewModel
    //    {
    //        Id = dto.Id,
    //        CategoryId = dto.CategoryId,

    //        CourseUpdate = new CourseUpdateRequestDto
    //        {
    //            Title = dto.Title,
    //            Author = dto.Author,
    //            Price = dto.Price,
    //            DiscountPrice = dto.DiscountPrice,
    //            Hours = dto.Hours,
    //            LikesInProcent = dto.LikesInProcent,
    //            LikesInNumbers = dto.LikesInNumbers,
    //            IsBestSeller = dto.IsBestSeller,
    //            CategoryName = dto.Category              
    //        }
    //    };
    //}

    //public static ContactRequestViewModel MapToViewModel(ContactRequestDto dto)
    //{
    //    return new ContactRequestViewModel
    //    {
    //        Id = dto.Id,
    //        FullName = dto.FullName,
    //        Email = dto.Email,
    //        Service = dto.Service,
    //        Message = dto.Message,
    //        CreatedAt = dto.CreatedAt
    //    };
    //}

    //public static ContactRequestDto MapToDto(ContactRequestViewModel viewModel)
    //{
    //    return new ContactRequestDto
    //    {
    //        Id = viewModel.Id,
    //        FullName = viewModel.FullName,
    //        Email = viewModel.Email,
    //        Service = viewModel.Service!,
    //        Message = viewModel.Message,
    //        CreatedAt = viewModel.CreatedAt
    //    };
    //}

    //public static SubscriberViewModel MapToViewModel(SubscriberDto dto)
    //{
    //    return new SubscriberViewModel
    //    {
    //        Id = dto.Id,
    //        Email = dto.Email,
    //        DailyNewsLetter = dto.DailyNewsLetter,
    //        AdvertisingUpdates = dto.AdvertisingUpdates,
    //        EventUpdates = dto.EventUpdates,
    //        WeekInReview = dto.WeekInReview,
    //        StartupsWeekly = dto.StartupsWeekly,
    //        Podcasts = dto.Podcasts,
    //        CreatedAt = dto.CreatedAt,
    //        IsRegisteredUser = dto.IsRegisteredUser,
    //        FullName = dto.FullName
    //    };
    //}

    //public static SubscriberDto MapToDto(SubscriberViewModel viewModel)
    //{
    //    return new SubscriberDto
    //    {
    //        Id = viewModel.Id,
    //        Email = viewModel.Email,
    //        DailyNewsLetter = viewModel.DailyNewsLetter,
    //        AdvertisingUpdates = viewModel.AdvertisingUpdates,
    //        EventUpdates = viewModel.EventUpdates,
    //        WeekInReview = viewModel.WeekInReview,
    //        StartupsWeekly = viewModel.StartupsWeekly,
    //        Podcasts = viewModel.Podcasts,
    //        CreatedAt = viewModel.CreatedAt,
    //        IsRegisteredUser = viewModel.IsRegisteredUser,
    //        FullName = viewModel.FullName
    //    };
    //}

    public static CourseComponent MapToCourseComponent(UserCourseDto dto)
    {
        return new CourseComponent
        {
            Id = dto.CourseId,
            Title = dto.Title,
            CourseImage = new ImageComponent
            {
                ImageUrl = dto.CourseImage,
                AltText = dto.Title
            },
            CourseAuthor = dto.CourseAuthor,
            CoursePrice = dto.CoursePrice.ToString(),
            CourseDiscountPrice = dto.CourseDiscountPrice?.ToString(),
            LikesInProcent = dto.LikesInProcent.ToString(),
            LikesInNumbers = dto.LikesInNumbers.ToString(),
            CourseHours = dto.CourseHours.ToString(),
            IsBestSeller = dto.IsBestSeller,
            CategoryName = dto.CategoryName
        };
    }

    //public static CourseViewModel MapToViewModel(CourseDto dto)
    //{
    //    return new CourseViewModel
    //    {
    //        Id = dto.Id,
    //        Title = dto.Title,
    //        Author = dto.Author,
    //        Price = dto.Price,
    //        DiscountPrice = dto.DiscountPrice,
    //        Hours = dto.Hours,
    //        LikesInProcent = dto.LikesInProcent,
    //        LikesInNumbers = dto.LikesInNumbers,
    //        IsBestSeller = dto.IsBestSeller,
    //        Category = dto.CategoryName!,
    //        CourseImage = new ImageComponent
    //        {
    //            ImageUrl = dto.CourseImage,
    //            AltText = dto.Title
    //        }
    //    };
    //}

    //public static CourseComponent MapToCourseComponent(CourseViewModel viewModel)
    //{
    //    return new CourseComponent
    //    {
    //        Id = viewModel.Id,
    //        CourseImage = new ImageComponent
    //        {
    //            ImageUrl = viewModel.CourseImage!.ImageUrl,
    //            AltText = viewModel.CourseImage.AltText
    //        },
    //        Title = viewModel.Title,
    //        CourseAuthor = viewModel.Author,
    //        CoursePrice = viewModel.Price.ToString(),
    //        CourseDiscountPrice = viewModel.DiscountPrice?.ToString(),
    //        CourseHours = viewModel.Hours.ToString(),
    //        LikesInProcent = viewModel.LikesInProcent.ToString(),
    //        LikesInNumbers = viewModel.LikesInNumbers.ToString(),
    //        IsBestSeller = viewModel.IsBestSeller,
    //        CategoryName = viewModel.Category
    //    };
    //}

    //public static CategoryViewModel MapToViewModel(CategoryDto dto)
    //{
    //    return new CategoryViewModel
    //    {
    //        Id = dto.Id,
    //        CategoryName = dto.CategoryName
    //    };
    //}

    public static CourseIntroViewModel MapToCourseIntroViewModel(CourseGetRequestDto dto)
    {
        var tags = new List<string>();
        if (dto.IsBestSeller)
        {
            tags.Add("Best Seller");
        }
        tags.Add("Digital");

        return new CourseIntroViewModel
        {
            Id = "Course-intro",
            BackgroundImage = new ImageComponent
            {
                ImageUrl = dto.CourseImage,
                AltText = $"Image of the course {dto.Title}"
            },
            HomeLink = new LinkComponent { ControllerName = "Home", ActionName = "Index", Url = "Home" },
            CoursesLink = new LinkComponent { ControllerName = "AvailableCourses", ActionName = "Index", Url = "availablecourses" },
            CourseView = dto.Title,
            Tags = tags,
            CourseTitle = dto.Title,
            StarRating = new StarRatingComponent
            {
                StarIcon = new ImageComponent { ImageUrl = "/Assets/Icons/ui/star-icon.svg", AltText = "Star" },
                EmptyStarIcon = new ImageComponent { ImageUrl = "/Assets/Icons/ui/empty-star-icon.svg", AltText = "Empty Star" },
                NumberOfStars = 4,
                TotalStars = 5
            },
            Reviews = $"({dto.LikesInNumbers} reviews)",
            Likes = $"{dto.LikesInNumbers}K likes",
            CourseHours = $"{dto.Hours} hours",
            CourseDescription = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
            AuthorImage = new ImageComponent 
            { 
                ImageUrl = "/Assets/Images/SelectedCourse/author-image-small.svg", 
                AltText = $"Image of the author {dto.Author}" 
            },
            AuthorText = "Created by",
            AuthorName = dto.Author
        };
    }

    public static CourseDetailsViewModel MapToCourseDetailsViewModel(CourseGetRequestDto dto)
    {
        return new CourseDetailsViewModel
        {
            Id = dto.Id,
            Title = dto.Title,
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
            }
        ],
            Includes = new CourseIncludes
            {
                Title = "This course includes:",
                OriginalPrice = dto.Price.ToString(),
                SalePrice = dto.DiscountPrice?.ToString() ?? string.Empty,
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
    }

    public static InstructorInfoViewModel MapToInstructorInfoViewModel(CourseGetRequestDto dto)
    {
        return new InstructorInfoViewModel
        {
            InstructorImage = new ImageComponent
            {
                ImageUrl = "/Assets/Images/SelectedCourse/author-image-big.svg",
                AltText = $"Image of instructor {dto.Author}"
            },
            Title = "Learn from the best",
            InstructorName = dto.Author,
            Biography = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
            YoutubeLink = new LinkComponent
            {
                Url = "https://www.youtube.com",
                Text = "240k subscribers",
                Icon = new ImageComponent
                {
                    ImageUrl = "/Assets/Icons/social/youtube-icon.svg",
                    AltText = "Youtube Icon"
                }
            },
            FacebookLink = new LinkComponent
            {
                Url = "https://www.facebook.com",
                Text = "180k followers",
                Icon = new ImageComponent
                {
                    ImageUrl = "/Assets/Icons/social/facebook-icon.svg",
                    AltText = "Facebook Icon"
                }
            }
        };
    }
}