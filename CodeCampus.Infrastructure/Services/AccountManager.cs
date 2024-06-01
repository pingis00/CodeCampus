using CodeCampus.Infrastructure.Contexts;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Security.Claims;

namespace CodeCampus.Infrastructure.Services;

public class AccountManager(UserManager<UserEntity> userManager, DataContext dataContext, IFileUploadService fileUploadService, ILogger<AccountManager> logger) : IAccountManager
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly DataContext _dataContext = dataContext;
    private readonly IFileUploadService _fileUploadService = fileUploadService;
    private readonly ILogger<AccountManager> _logger = logger;

    public async Task<bool> UploadUserProfileImageAsync(ClaimsPrincipal user, IFormFile file)
    {
        try
        {
            if (user != null && file != null && file.Length != 0)
            {
                var userEntity = await _userManager.GetUserAsync(user);
                if (userEntity != null)
                {
                    var filePath = await _fileUploadService.UploadFileAsync(file, "profileimages");

                    userEntity.ProfileImage = filePath;
                    _dataContext.Update(userEntity);
                    await _dataContext.SaveChangesAsync();

                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading user profile image");
        }
        return false;
    }
}
