using CodeCampus.Infrastructure.Contexts;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Interfaces.Repositories;

using Microsoft.Extensions.Logging;

namespace CodeCampus.Infrastructure.Repositories;

public class ContactRepository(DataContext context, ILogger<ContactRepository> logger) : Base<ContactEntity>(context, logger), IContactRepository
{
}
