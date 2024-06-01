using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Interfaces.Repositories;
using CodeCampus.Infrastructure.Interfaces.Services;
using CodeCampus.Infrastructure.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


namespace CodeCampus.Infrastructure.Services;

public class SubscribeService(ISubscribeRepository subscribeRepository, UserManager<UserEntity> userManager, ILogger<SubscribeService> logger) : ISubscribeService
{
    private readonly ISubscribeRepository _subscribeRepository = subscribeRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly ILogger<SubscribeService> _logger = logger;

    public async Task<ResponseResult> IsEmailSubscribedAsync(string email)
    {
        try
        {
            var result = await _subscribeRepository.IsEmailSubscribedAsync(email);

            if (result.Status == StatusCode.OK && result.ContentResult is bool isSubscribed && isSubscribed)
            {
                return new ResponseResult { Status = StatusCode.EXISTS, Message = "Email is already subscribed." };
            }

            return new ResponseResult { Status = StatusCode.NOT_FOUND, Message = "Email is not subscribed." };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while checking if email is subscribed in service");
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }

    public async Task<ResponseResult> SubscribeEmailAsync(SubscribeRequest request, string? userId)
    {
        try
        {
            var existsResult = await _subscribeRepository.IsEmailSubscribedAsync(request.Email);
            if (existsResult.Status == StatusCode.OK && existsResult.ContentResult is bool isSubscribed && isSubscribed)
            {
                return new ResponseResult { Status = StatusCode.EXISTS, Message = "Email is already subscribed." };
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                userId = user.Id;
            }

            var subscription = SubscribeFactory.Create(request, userId);
            var result = await _subscribeRepository.CreateOneAsync(subscription);

            if (user != null)
            {
                user.IsSubscribed = true;
                await _userManager.UpdateAsync(user);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while subscribing email in service");
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }

    public async Task<ResponseResult> GetAllSubscribersAsync()
    {
        try
        {
            var result = await _subscribeRepository.GetAllAsync();
            return new ResponseResult { Status = result.Status, ContentResult = result.ContentResult };
        }
        catch (Exception ex)
        {
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }

    public async Task<ResponseResult> GetSubscriberByIdAsync(int id)
    {
        try
        {
            var result = await _subscribeRepository.GetOneAsync(c => c.Id == id);
            if (result.Status != StatusCode.OK)
            {
                return new ResponseResult { Status = StatusCode.NOT_FOUND, Message = "Subscriber not found." };
            }
            var subscriber = (SubscribeEntity)result.ContentResult!;
            var subscriberViewModel = SubscribeFactory.Create(subscriber);

            if (subscriber.IsRegisteredUser && subscriber.UserId != null)
            {
                var user = await _userManager.FindByIdAsync(subscriber.UserId);
                if (user != null)
                {
                    subscriberViewModel.FullName = $"{user.FirstName} {user.LastName}";
                }
            }
            return new ResponseResult { Status = StatusCode.OK, ContentResult = subscriberViewModel };

        }
        catch (Exception ex)
        {
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }

    public async Task<ResponseResult> UnsubscribeEmailAsync(int id)
    {
        try
        {
            var result = await _subscribeRepository.DeleteOneAsync(c => c.Id == id);
            if (result.Status != StatusCode.OK)
            {
                return new ResponseResult { Status = StatusCode.NOT_FOUND, Message = "Subscriber not found." };
            }
            return result;
        }
        catch (Exception ex)
        {
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }
}

