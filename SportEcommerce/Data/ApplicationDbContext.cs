using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportEcommerce.Models.Orders;
using SportEcommerce.Models.Product;
using SportEcommerce.Models.User;

namespace SportEcommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for the models
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductInstance> ProductInstances { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the primary key for Seller
            modelBuilder.Entity<Seller>()
                .HasOne(s => s.User)
                .WithOne(u => u.Seller)
                .HasForeignKey<Seller>(s => s.Id);

            // Configure the primary key for Buyer
            modelBuilder.Entity<Buyer>()
                .HasOne(b => b.User)
                .WithOne(u => u.Buyer)
                .HasForeignKey<Buyer>(b => b.Id);

            // Initial roles seeding
            modelBuilder.Entity<ApplicationRole>().HasData
                (
                new ApplicationRole() { Id = "35a5de8a-8f7b-4806-a1fe-c5a32ed430e8", IsDefault=true ,Name = "Buyer", NormalizedName = "BUYER"},
                new ApplicationRole() { Id = "3d99eca7-c218-44fb-a64d-163e486acb1b", Name = "Seller", NormalizedName = "SELLER"}
                );

            // Seed countries
            modelBuilder.Entity<Country>().HasData
                (
                    new Country() { Id=1, Name = "Colombia"},
                    new Country() { Id=2, Name = "United States"},
                    new Country() { Id=3, Name = "Mexico"},
                    new Country() { Id=4, Name = "Argentina"},
                    new Country() { Id=5, Name = "United Kindom"},
                    new Country() { Id=6, Name = "Australia"}
                );
        }
    }
}

