using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Entities;

namespace CodeCampus.Infrastructure.Factories;

public static class SubscribeFactory
{
    public static SubscribeEntity Create(SubscribeRequest request, string? userId)
    {
        return new SubscribeEntity
        {
            Email = request.Email,
            DailyNewsLetter = request.DailyNewsLetter,
            AdvertisingUpdates = request.AdvertisingUpdates,
            EventUpdates = request.EventUpdates,
            WeekInReview = request.WeekInReview,
            StartupsWeekly = request.StartupsWeekly,
            Podcasts = request.Podcasts,
            IsRegisteredUser = !string.IsNullOrEmpty(userId),
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static SubscriberDto Create(SubscribeEntity entity)
    {
        return new SubscriberDto
        {
            Id = entity.Id,
            Email = entity.Email,
            DailyNewsLetter = entity.DailyNewsLetter,
            AdvertisingUpdates = entity.AdvertisingUpdates,
            EventUpdates = entity.EventUpdates,
            WeekInReview = entity.WeekInReview,
            StartupsWeekly = entity.StartupsWeekly,
            Podcasts = entity.Podcasts,
            CreatedAt = entity.CreatedAt,
            IsRegisteredUser = entity.IsRegisteredUser,
            FullName = null
        };
    }
}
