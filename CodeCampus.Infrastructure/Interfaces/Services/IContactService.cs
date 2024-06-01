using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Responses;

namespace CodeCampus.Infrastructure.Interfaces.Services;

public interface IContactService
{
    Task<ResponseResult> SubmitContactRequestAsync(ContactRequestDto request, string? userId);
    Task<ResponseResult> GetAllContactsAsync();
    Task<ResponseResult> GetContactByIdAsync(int id);
    Task<ResponseResult> DeleteContactRequestAsync(int id);
}
