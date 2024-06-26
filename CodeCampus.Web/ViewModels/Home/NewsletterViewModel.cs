﻿using System.ComponentModel.DataAnnotations;

namespace CodeCampus.Web.ViewModels.Home;

public class NewsletterViewModel
{
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email address", Prompt = "Your email")]
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$", ErrorMessage = "Enter a valid email")]
    public string Email { get; set; } = null!;

    public bool DailyNewsLetter { get; set; }
    public bool AdvertisingUpdates { get; set; }
    public bool EventUpdates { get; set; }
    public bool WeekInReview { get; set; }
    public bool StartupsWeekly { get; set; }
    public bool Podcasts { get; set; }
}
