using CodeCampus.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeCampus.Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<SubscribeEntity> Subscribers { get; set; }
    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<UserCourseEntity> UserCourses { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Address)
                .WithMany(a => a.Users)
                .HasForeignKey(u => u.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.Subscriptions)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ContactEntity>()
            .Property(c => c.FullName)
            .IsRequired();

        modelBuilder.Entity<ContactEntity>()
            .Property(c => c.Email)
            .IsRequired();

        modelBuilder.Entity<ContactEntity>()
            .Property(c => c.Message)
            .IsRequired();

        modelBuilder.Entity<UserCourseEntity>()
            .HasKey(uc => new { uc.UserId, uc.CourseId });

        modelBuilder.Entity<UserCourseEntity>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserCourses)
            .HasForeignKey(uc => uc.UserId);

        modelBuilder.Entity<UserCourseEntity>()
            .HasOne(uc => uc.Course)
            .WithMany(c => c.UserCourses)
            .HasForeignKey(uc => uc.CourseId);

        modelBuilder.Entity<CourseEntity>()
            .Property(c => c.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<CourseEntity>()
            .Property(c => c.DiscountPrice)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<CourseEntity>()
                .HasOne(c => c.Category)
                .WithMany()
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

    }
}