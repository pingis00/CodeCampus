using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Responses;

namespace CodeCampus.Infrastructure.Interfaces.Services;

public interface ISubscribeService
{
    Task<ResponseResult> IsEmailSubscribedAsync(string email);
    Task<ResponseResult> SubscribeEmailAsync(SubscribeRequest request, string? userId);
    Task<ResponseResult> GetAllSubscribersAsync();
    Task<ResponseResult> GetSubscriberByIdAsync(int id);
    Task<ResponseResult> UnsubscribeEmailAsync(int id);
}
