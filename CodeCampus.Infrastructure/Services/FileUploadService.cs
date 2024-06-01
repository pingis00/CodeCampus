using CodeCampus.Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CodeCampus.Infrastructure.Services;

public class FileUploadService(IConfiguration configuration, ILogger<FileUploadService> logger) : IFileUploadService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly ILogger<FileUploadService> _logger = logger;

    public async Task<string> UploadFileAsync(IFormFile file, string subDirectory)
    {
        try
        {
            var directoryPath = _configuration["FileUploadPath"];
            if (!string.IsNullOrEmpty(subDirectory))
            {
                directoryPath = Path.Combine(directoryPath!, subDirectory);
            }

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath!);
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(directoryPath!, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "File upload failed.");
            return null!;
        }
    }
}
