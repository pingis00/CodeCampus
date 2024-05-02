using System.ComponentModel.DataAnnotations;

namespace CodeCampus.Infrastructure.Models;

public class AccountDetailsAddressInfoModel
{
    [DataType(DataType.Text)]
    [Display(Name = "Address line 1", Prompt = "Enter your address line", Order = 0)]
    [Required(ErrorMessage = "Address is required")]
    [MinLength(2, ErrorMessage = "Address requires at least 2 characters")]
    public string Addressline_1 { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Address line 2 (Optional)", Prompt = "Enter your second address line", Order = 1)]
    [MinLength(2, ErrorMessage = "Address requires at least 2 characters")]
    public string? Addressline_2 { get; set; }

    [DataType(DataType.PostalCode)]
    [Display(Name = "Postal code", Prompt = "Enter your postal code", Order = 2)]
    [Required(ErrorMessage = "Postal code is required")]
    [RegularExpression(@"^[0-9a-zA-Z\s]{3,10}$", ErrorMessage = "Invalid postalcode")]
    public string PostalCode { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "City", Prompt = "Enter your City", Order = 3)]
    [Required(ErrorMessage = "City is required")]
    [MinLength(2, ErrorMessage = "City requires at least 2 characters")]
    public string City { get; set; } = null!;
}
