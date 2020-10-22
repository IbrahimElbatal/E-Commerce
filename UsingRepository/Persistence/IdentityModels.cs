using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using UsingRepository.Core.Models;

namespace UsingRepository.Persistence
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class HeroContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public HeroContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // order user
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.User)
                .WithMany().WillCascadeOnDelete(false);


            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.CityId).IsOptional();

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.CountryId).IsOptional();

            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(o => o.City)
                .WithMany().WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }

        public static HeroContext Create()
        {
            return new HeroContext();
        }
    }
}