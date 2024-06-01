using CodeCampus.Infrastructure.Contexts;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeCampus.Infrastructure.Repositories;

public class AddressRepository(DataContext context, ILogger<AddressRepository> logger) : Base<AddressEntity>(context, logger), IAddressRepository
{
}
