﻿using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Models;
using System.Diagnostics;

namespace CodeCampus.Infrastructure.Factories;

public class AddressFactory
{
    public static AddressEntity Create()
    {
        try
        {
            return new AddressEntity();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
        return null!;
    }

    public static AddressEntity Create(string addressline_1, string addressLine_2, string postalCode, string city)
    {
        try
        {
            return new AddressEntity
            {
                AddressLine_1 = addressline_1,
                AddressLine_2 = addressLine_2,
                PostalCode = postalCode,
                City = city
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
        return null!;
    }

    public static AddressModel Create(AddressEntity entity)
    {
        try
        {
            return new AddressModel
            {
                Id = entity.Id,
                AddressLine_1 = entity.AddressLine_1,
                AddressLine_2 = entity.AddressLine_2,
                PostalCode = entity.PostalCode,
                City = entity.City
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
        return null!;
    }
}
