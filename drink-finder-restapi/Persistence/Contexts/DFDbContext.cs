using drink_finder_restapi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace drink_finder_restapi.Persistence.Contexts
{
    public class DFDbContext : DbContext
    {

        public DFDbContext(DbContextOptions<DFDbContext> options) : base(options) { }

        public DbSet<DrinkCategory> drinkCategories { get; set; }
        public DbSet<Drink> drinks { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Establishment> establishments { get; set; }
        public DbSet<EstablishmentDrink> establishmentDrinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // City
            modelBuilder.Entity<City>()
                .HasKey(c => c.Id);

            // DrinkCategory
            modelBuilder.Entity<DrinkCategory>()
                .HasKey(dc => dc.id);

            // Drink
            modelBuilder.Entity<Drink>()
                .HasKey(d => d.Id);
            modelBuilder.Entity<Drink>()
                .HasOne(d => d.drinkCategory)
                .WithMany(dc => dc.drinks)
                .HasForeignKey(d => d.drinkCategoryId);

            // Establishment
            modelBuilder.Entity<Establishment>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Establishment>()
                .HasOne(e => e.City)
                .WithMany(c => c.Establishments)
                .HasForeignKey(e => e.CityId);
            modelBuilder.Entity<Establishment>()
                .HasOne(e => e.User)
                .WithMany(u => u.establishments)
                .HasForeignKey(e => e.UserId);

            // EstablishmentDrink
            modelBuilder.Entity<EstablishmentDrink>()
                .HasKey(ed => new { ed.EstablishemntId, ed.DrinkId });
            modelBuilder.Entity<EstablishmentDrink>()
                .HasOne(ed => ed.establishment)
                .WithMany(e => e.establishemntDrinks)
                .HasForeignKey(ed => ed.EstablishemntId);
            modelBuilder.Entity<EstablishmentDrink>()
                .HasOne(ed => ed.drink)
                .WithMany(d => d.establishemntDrinks)
                .HasForeignKey(ed => ed.DrinkId);
        }
    }
}
