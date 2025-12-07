using Microsoft.EntityFrameworkCore;
using FOODCMS.API.Entities;

namespace FOODCMS.API.Data;

public class RestaurantDbContext : DbContext
{
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("users");
            e.HasKey(u => u.UserId);

            e.Property(u => u.UserId).HasColumnName("user_id");
            e.Property(u => u.Username).HasColumnName("username");
            e.Property(u => u.PasswordHash).HasColumnName("password_hash");
            e.Property(u => u.FullName).HasColumnName("full_name");
            e.Property(u => u.Email).HasColumnName("email");
            e.Property(u => u.Role).HasColumnName("role");
            e.Property(u => u.IsActive).HasColumnName("is_active");
            e.Property(u => u.CreatedOn).HasColumnName("created_on");
            e.Property(u => u.CreatedBy).HasColumnName("created_by");
            e.Property(u => u.ModifiedOn).HasColumnName("modified_on");
            e.Property(u => u.ModifiedBy).HasColumnName("modified_by");
        });
    }
}
