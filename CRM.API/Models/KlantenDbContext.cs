using Microsoft.EntityFrameworkCore;
using CRM.Models;
using CRM.Models.Enums;
namespace CRM.API.Models
{
    public class KlantenDbContext : DbContext
    {
        public KlantenDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Klant> Klanten { get; set; }
        public DbSet<GebruikersAccount> GebruikersAccounts { get; set; }
        public DbSet<Adres> Adressen { get; set; }
        public DbSet<Land> Landen { get; set; }
        public DbSet<Factuur> Facturen { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision for Factuur.Prijs
            modelBuilder.Entity<Factuur>()
                .Property(f => f.Prijs)
                .HasPrecision(18, 2);

            // Configure decimal precision for Klant.BtwPercentage
            modelBuilder.Entity<Klant>()
                .Property(k => k.BtwPercentage)
                .HasPrecision(5, 2);

            // Configure Klant -> Adres relationship (optional)
            modelBuilder.Entity<Klant>()
                .HasOne(k => k.Adres)
                .WithMany()
                .HasForeignKey(k => k.AdresId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure Adres -> Land relationship
            modelBuilder.Entity<Adres>()
                .HasOne(a => a.Land)
                .WithMany()
                .HasForeignKey(a => a.LandCode)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data for GebruikersAccount
            modelBuilder.Entity<GebruikersAccount>().HasData(
                new GebruikersAccount
                {
                    Id = 1,
                    Email = "admin@admin.com",
                    Wachtwoord = "admin123",
                    SecurityLevel = SecurityLevel.Admin
                },
                new GebruikersAccount
                {
                    Id = 2,
                    Email = "owner@owner.com",
                    Wachtwoord = "owner123",
                    SecurityLevel = SecurityLevel.Owner
                },
                new GebruikersAccount
                {
                    Id = 3,
                    Email = "user@user.com",
                    Wachtwoord = "guest123",
                    SecurityLevel = SecurityLevel.User
                }
            );
        }


    }
}
