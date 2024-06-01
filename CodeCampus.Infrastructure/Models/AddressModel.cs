﻿namespace CodeCampus.Infrastructure.Models;

public class AddressModel
{
    public int Id { get; set; }
    public string AddressLine_1 { get; set; } = null!;
    public string? AddressLine_2 { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}

