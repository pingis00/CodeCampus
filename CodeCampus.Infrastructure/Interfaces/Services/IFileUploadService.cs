using Microsoft.AspNetCore.Http;

namespace CodeCampus.Infrastructure.Interfaces.Services;

public interface IFileUploadService
{
    Task<string> UploadFileAsync(IFormFile file, string subDirectory);
}
