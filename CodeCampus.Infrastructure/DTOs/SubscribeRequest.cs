using System.ComponentModel.DataAnnotations;

namespace CodeCampus.Infrastructure.DTOs;

public class SubscribeRequest
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string Email { get; set; } = null!;
    public bool DailyNewsLetter { get; set; }
    public bool AdvertisingUpdates { get; set; }
    public bool EventUpdates { get; set; }
    public bool WeekInReview { get; set; }
    public bool StartupsWeekly { get; set; }
    public bool Podcasts { get; set; }
}
