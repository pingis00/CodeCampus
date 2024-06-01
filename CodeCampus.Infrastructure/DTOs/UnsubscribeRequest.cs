using System.ComponentModel.DataAnnotations;

namespace CodeCampus.Infrastructure.DTOs;

public class UnsubscribeRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}
