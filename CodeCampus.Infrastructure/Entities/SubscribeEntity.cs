namespace CodeCampus.Infrastructure.Entities;

public class SubscribeEntity
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public bool DailyNewsLetter { get; set; }
    public bool AdvertisingUpdates { get; set; }
    public bool EventUpdates { get; set; }
    public bool WeekInReview { get; set; }
    public bool StartupsWeekly { get; set; }
    public bool Podcasts { get; set; }
    public bool IsRegisteredUser { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UserId { get; set; }
    public UserEntity? User { get; set; }
}
