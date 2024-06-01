using Microsoft.AspNetCore.Identity;

namespace CodeCampus.Infrastructure.Entities;

public class UserEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;

    [ProtectedPersonalData]
    public string? Bio { get; set; }

    public int? AddressId { get; set; }
    public AddressEntity? Address { get; set; }

    public bool IsExternalAccount { get; set; } = false;

    public string? ProfileImage { get; set; }

    public bool IsSubscribed { get; set; } = false;

    public ICollection<SubscribeEntity> Subscriptions { get; set; } = [];

    public ICollection<UserCourseEntity> UserCourses { get; set; } = [];
}

