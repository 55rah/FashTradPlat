using FashTradPlat.Areas.Identity.Data;
using FashTradPlat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace FashTradPlat.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
        });
        // Creates admin role for authorization
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" }
            );

        var harsher = new PasswordHasher<ApplicationUser>();
        builder.Entity<ApplicationUser>().HasData(

            new ApplicationUser
            {
                Id = "1",
                FirstName = "Admin",
                LastName = "User",
                UserName = "admin@ftp.com",
                NormalizedUserName = "ADMIN@FTP.COM",
                Email = "admin@ftp.com",
                NormalizedEmail = "ADMIN@FTP.COM",
                EmailConfirmed = true,
                PasswordHash = harsher.HashPassword(null, "Abcd!1232")
                // Credentials for Admin
                // admin@ftp.com
                // Abcd!1232
            }
        );

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { RoleId = "1", UserId = "1" }
        );

    }

    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(255);
            builder.Property(u => u.LastName).HasMaxLength(255);
        }
    }
}