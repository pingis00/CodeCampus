using CodeCampus.Web.Models.Components;
using static CodeCampus.Web.ViewModels.Home.DownloadViewModel;

namespace CodeCampus.Web.ViewModels.Home;

public class HomeIndexViewModel
{
    public ShowcaseViewModel Showcase { get; set; } = new ShowcaseViewModel
    {
        Id = "showcase",
        ShowcaseImage = new() { ImageUrl = "/Assets/Images/Homepage/taskmaster-image.svg", AltText = "Task Management Assistant" },
        Title = "Task Management Assistant You Gonna Love",
        Text = "We offer you a new generation of task management system. Plan, manage & track all your tasks in one flexible tool.",
        Link = new() { ControllerName = "Downloads", ActionName = "Index", Text = "Get started for free" },
        BrandsText = "Largest companies use our tool to work efficiently",
        Brands =
    [
        new() { ImageUrl = "/Assets/Icons/brands/brand_1.svg", AltText = "Brand Name 1"},
        new() { ImageUrl = "/Assets/Icons/brands/brand_2.svg", AltText = "Brand Name 2"},
        new() { ImageUrl = "/Assets/Icons/brands/brand_3.svg", AltText = "Brand Name 3"},
        new() { ImageUrl = "/Assets/Icons/brands/brand_4.svg", AltText = "Brand Name 4"}
    ]
    };

    public FeaturesViewModel Features { get; set; } = new FeaturesViewModel
    {
        Title = "What Do You Get With Our Tool?",
        Text = "Make sure all your tasks are organized so you can set the priorities and focus on important",
        IconFeatures =
        [
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "Assets/Icons/ui/chat.svg", AltText = "Speech bubble icon representing comments on tasks." },
                Title = "Comments on Tasks",
                Description = "Id mollis consectetur congue egestas egestas suspendisse blandit justo."
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "/Assets/Icons/ui/presentation.svg", AltText = "Chart icon representing task analytics." },
                Title = "Tasks Analytics",
                Description = "Non imperdiet facilisis nulla tellus Morbi scelerisque eget adipiscing vulputate."
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "/Assets/Icons/ui/add-group.svg", AltText = "Figures connected by lines icon representing multiple assignees." },
                Title = "Multiple Assignees",
                Description = "A elementum, imperdiet enim, pretium etiam facilisi in aenean quam mauris."
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "/Assets/Icons/ui/bell.svg", AltText = "Bell icon representing notifications." },
                Title = "Notifications",
                Description = "Diam, suspendisse velit cras ac. Lobortis diam volutpat, eget pellentesque viverra."
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "/Assets/Icons/ui/tests.svg", AltText = "Checklist icon representing sections and subtasks." },
                Title = "Sections & Subtasks",
                Description = "Mi feugiat hac id in. Sit elit placerat lacus nibh lorem ridiculus lectus."
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "/Assets/Icons/ui/shield.svg", AltText = "Shield icon representing data security." },
                Title = "Data Security",
                Description = "Aliquam malesuada neque eget elit nulla vestibulum nunc cras."
            }
        ]
    };

    public ThemeSwitchViewModel ThemeSwitch { get; set; } = new ThemeSwitchViewModel
    {
        Id = "ThemeSwitch",
        TitleLeft = "Switch Between",
        TitleRight = "Light & Dark Mode",
        Image = new ImageComponent { ImageUrl = "/Assets/Images/Homepage/theme-switch-imac.svg", AltText = "Screenshots of the app showing light and dark mode" },
        SwitchButton = new ImageComponent { ImageUrl = "/Assets/Images/Homepage/slider-button.svg", AltText = "Switch between light and dark mode" }
    };

    public WorkManagementViewModel WorkManagement { get; set; } = new WorkManagementViewModel
    {
        Id = "WorkManagement",
        Image = new ImageComponent { ImageUrl = "Assets/Images/Homepage/dashboard-features.svg", AltText = "A dashboard showing various project management features" },
        Title = "Manage Your Work",
        Features =
        [
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "Assets/Icons/ui/check-circle-icon.svg", AltText = ""},
                Description = "Powerful project management"
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "Assets/Icons/ui/check-circle-icon.svg", AltText = ""},
                Description = "Transparent work management"
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "Assets/Icons/ui/check-circle-icon.svg", AltText = ""},
                Description = "Manage work & focus on the most important tasks"
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "Assets/Icons/ui/check-circle-icon.svg", AltText = ""},
                Description = "Track your progress with interactive charts"
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "Assets/Icons/ui/check-circle-icon.svg", AltText = ""},
                Description = "Easiest way to track time spent on tasks"
            }
        ],
        Link = new()
        {
            ControllerName = "LearnMore",
            ActionName = "Index",
            Text = "Learn more",
            Icon = new ImageComponent
            {
                ImageUrl = "Assets/Icons/ui/learn-more-arrow.svg",
                AltText = "right arrow"
            }
        },
    };

    public DownloadViewModel Download { get; set; } = new DownloadViewModel
    {
        Id = "Download",
        MobileImage = new ImageComponent { ImageUrl = "Assets/Images/Downloads/mobile-app-screenshots.svg", AltText = "Screenshots of TaskMaster mobile app interface displaying a daily task overview and navigation icons." },
        Title = "Download Our App for Any Devices:",
        AppLinks =
        [
            new AppLinkComponent {
                StoreName = "App Store",
                StarRating = new StarRatingComponent {
                    StarIcon = new ImageComponent {
                        ImageUrl = "Assets/Icons/ui/star-icon.svg", AltText = "star rating"
                    },
                    NumberOfStars = 5
                },
                EditorChoiceText = "Editor’s Choice",
                RatingText = "Rating 4.7, 187K+ reviews",
                Link = new LinkComponent
                {
                    Url = "https://apps.apple.com/app/your-app-id",
                    Icon = new ImageComponent {
                        ImageUrl = "Assets/Icons/brands/app-store-badge-light.svg",
                        DarkModeImageUrl = "Assets/Icons/brands/app-store-badge-dark.svg",
                        AltText = "App store badge"
                    }
                }
            },

            new AppLinkComponent {
                StoreName = "Google Play",
                StarRating = new StarRatingComponent {
                    StarIcon = new ImageComponent {
                        ImageUrl = "Assets/Icons/ui/star-icon.svg", AltText = "star rating" },
                    NumberOfStars = 5
                },
                EditorChoiceText = "App of the Day",
                RatingText = "rating 4.8, 30K+ reviews",
                Link = new LinkComponent
                {
                    Url = "https://play.google.com/store/apps/details?id=your.app.id",
                    Icon = new ImageComponent {
                        ImageUrl = "Assets/Icons/brands/google-play-badge-light.svg",
                        DarkModeImageUrl = "Assets/Icons/brands/google-play-badge-dark.svg",
                        AltText = "Google play badge"
                    }
                }
            }
        ]
    };

    public IntegrationToolsViewModel IntegrationTools { get; set; } = new IntegrationToolsViewModel
    {
        Id = "IntegrationsTools",
        Title = "Integrate Top Work Tools",
        Subtitle = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin volutpat mollis egestas. Nam luctus facilisis ultrices. Pellentesque volutpat ligula est. Mattis fermentum, at nec lacus.",
        Tools =
        [
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "Assets/Icons/Integrations/google-icon.svg", AltText = "Googles ikon"},
                Description = "Lorem magnis pretium sed curabitur nunc facilisi nunc cursus sagittis."
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "Assets/Icons/Integrations/zoom-icon.svg", AltText = "Zooms ikon"},
                Description = "In eget a mauris quis. Tortor dui tempus quis integer est sit natoque placerat dolor."
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "Assets/Icons/Integrations/slack-icon.svg", AltText = "Slacks ikon"},
                Description = "Id mollis consectetur congue egestas egestas suspendisse blandit justo."
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "Assets/Icons/Integrations/gmail-icon.svg", AltText = "Gmails ikon"},
                Description = "Rutrum interdum tortor, sed at nulla. A cursus bibendum elit purus cras praesent."
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "Assets/Icons/Integrations/trello-icon.svg", AltText = "Trellos ikon"},
                Description = "Congue pellentesque amet, viverra curabitur quam diam scelerisque fermentum urna."
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "Assets/Icons/Integrations/mailchimp-icon.svg", AltText = "Mailchimps ikon"},
                Description = "A elementum, imperdiet enim, pretium etiam facilisi in aenean quam mauris."
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "Assets/Icons/Integrations/dropbox-icon.svg", AltText = "Dropbox ikon"},
                Description = "Ut in turpis consequat odio diam lectus elementum. Est faucibus blandit platea."
            },
            new IconComponent {
                Icon = new ImageComponent { ImageUrl = "Assets/Icons/Integrations/evernote-icon.svg", AltText = "Evernotes ikon"},
                Description = "Faucibus cursus maecenas lorem cursus nibh. Sociis sit risus id. Sit facilisis dolor arcu."
            }
        ]
    };

    public NewsletterViewModel Newsletter { get; set; } = new NewsletterViewModel();
}
