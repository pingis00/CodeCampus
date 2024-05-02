using CodeCampus.Infrastructure.Contexts;
using CodeCampus.Infrastructure.Entities;

namespace CodeCampus.Infrastructure.Repositories;

public class AddressRepository(DataContext context) : Base<AddressEntity>(context)
{
    private readonly DataContext _context = context;
}
