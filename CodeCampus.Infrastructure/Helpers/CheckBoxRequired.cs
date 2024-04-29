﻿using System.ComponentModel.DataAnnotations;

namespace CodeCampus.Infrastructure.Helpers;

public class CheckBoxRequired : ValidationAttribute
{
    public override bool IsValid(object? value) => value is bool b && b;
}
