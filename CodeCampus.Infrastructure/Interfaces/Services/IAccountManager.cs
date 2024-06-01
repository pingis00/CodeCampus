using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CodeCampus.Infrastructure.Interfaces.Services;

public interface IAccountManager
{
    Task<bool> UploadUserProfileImageAsync(ClaimsPrincipal user, IFormFile file);
}
