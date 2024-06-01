using CodeCampus.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeCampus.Web.ViewModels.Contact;

public class ContactFormViewModel
{
    public ContactFormModel Form { get; set; } = new();

    public IEnumerable<SelectListItem> AvailableServices { get; set; } =
    [
            new() { Value = "Fullstack", Text = "Fullstack" },
            new() { Value = "Web developer", Text = "Web developer" },
            new() { Value = "Data Science", Text = "Data Science" },
            new() { Value = "Creative CSS", Text = "Creative CSS" },
            new() { Value = "Learn JMETER", Text = "Learn JMETER" },
    ];
}