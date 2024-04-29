using CodeCampus.Infrastructure.Models;
using CodeCampus.Web.Models.Components;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeCampus.Web.ViewModels.Contact;

public class ContactViewModel
{
    public string? Id { get; set; } = "contact-us";
    public string HomeLink { get; set; } = "Home";
    public string ContactText { get; set; } = "Contact";
    public string HeadTitle { get; set; } = "Contact Us";
    public List<LinkComponent> ContactOptions { get; set; } =
        [
            new LinkComponent()
            {
                Title = "Email us",
                Text = "Please feel free to drop us a line. We will respond as soon as possible.",
                ActionName = "Leave a message",
                Url = "*",
                Icon = new ImageComponent { ImageUrl = "/Assets/Icons/ui/email-icon.svg", AltText = "icon of a letter"}
            },
            new LinkComponent()
            {
                Title = "Careers",
                Text = "Sit ac ipsum leo lorem magna nunc mattis maecenas non vestibulum.",
                ActionName = "Send an application",
                Url = "*",
                Icon = new ImageComponent { ImageUrl = "/Assets/Icons/ui/career-icon.svg", AltText = ""}
            }
        ];
    public string Title { get; set; } = "Get In Contact With Us";
    public LinkComponent Link { get; set; } = new() { ControllerName = "Contact", ActionName = "SubmitContactForm", Text = "Send Contact Request" };
    public ContactFormModel ContactForm { get; set; } = new ContactFormModel();
    public IEnumerable<SelectListItem> AvailableServices { get; set; } =
        [
            new() { Value = "Fullstack", Text = "Fullstack" },
            new() { Value = "Web developer", Text = "Web developer" },
            new() { Value = "Data Science", Text = "Data Science" },
            new() { Value = "Creative CSS", Text = "Creative CSS" },
            new() { Value = "Learn JMETER", Text = "Learn JMETER" },
        ];
    public string GoogleMapsEmbedUrl { get; set; } = null!;
}
