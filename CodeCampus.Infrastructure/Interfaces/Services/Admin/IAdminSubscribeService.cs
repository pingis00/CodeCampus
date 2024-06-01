using CodeCampus.Infrastructure.Responses;

namespace CodeCampus.Infrastructure.Interfaces.Services.Admin;

public interface IAdminSubscribeService
{
    Task<ResponseResult> GetAllAdminSubscribersAsync();
    Task<ResponseResult> GetAdminSubscriberByIdAsync(int id);
    Task<ResponseResult> DeleteAdminSubscriberAsync(int id);
}
