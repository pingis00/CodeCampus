using CodeCampus.Infrastructure.Responses;

namespace CodeCampus.Infrastructure.Interfaces.Services.Admin;

public interface IAdminContactService
{
    Task<ResponseResult> GetAllAdminContactsAsync();
    Task<ResponseResult> GetAdminContactByIdAsync(int id);
    Task<ResponseResult> DeleteAdminContactRequestAsync(int id);
}
