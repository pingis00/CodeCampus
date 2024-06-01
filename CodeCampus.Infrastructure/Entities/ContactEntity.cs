namespace CodeCampus.Infrastructure.Entities;

public class ContactEntity
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Service { get; set; }
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string? UserId { get; set; }
}
