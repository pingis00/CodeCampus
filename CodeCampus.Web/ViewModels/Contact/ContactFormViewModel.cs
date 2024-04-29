using CodeCampus.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeCampus.Web.ViewModels.Contact;

public class ContactFormViewModel
{
    public string Title { get; set; } = null!;
    public ContactFormModel ContactForm { get; set; } = new ContactFormModel();
    public IEnumerable<SelectListItem> AvailableServices { get; set; } = [];
}
