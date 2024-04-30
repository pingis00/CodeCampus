using CodeCampus.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeCampus.Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<AddressEntity> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>()
            .HasOne(u => u.Address)
            .WithOne(a => a.User)
            .HasForeignKey<AddressEntity>(a => a.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
