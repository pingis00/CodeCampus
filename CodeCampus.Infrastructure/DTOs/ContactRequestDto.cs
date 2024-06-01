using System.ComponentModel.DataAnnotations;

namespace CodeCampus.Infrastructure.DTOs;

public class ContactRequestDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Full name is required.")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Service is required.")]
    public string Service { get; set; } = null!;

    [Required(ErrorMessage = "Message is required.")]
    public string Message { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
