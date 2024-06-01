using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Models;

namespace CodeCampus.Infrastructure.Factories;

public static class ContactFactory
{
    public static ContactEntity Create(ContactRequestDto request, string? userId)
    {
        return new ContactEntity
        {
            FullName = request.FullName,
            Email = request.Email,
            Service = request.Service,
            Message = request.Message,
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };
    }

    public static ContactModel Create(ContactEntity entity)
    {
        return new ContactModel
        {
            Id = entity.Id,
            FullName = entity.FullName,
            Email = entity.Email,
            Service = entity.Service!,
            Message = entity.Message,
            CreatedAt = entity.CreatedAt
        };
    }
}
