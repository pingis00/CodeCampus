using CodeCampus.Infrastructure.Entities;

namespace CodeCampus.Infrastructure.Interfaces.Services.Admin;

public interface ITokenService
{
    Task<string> GenerateToken(UserEntity user);
}
