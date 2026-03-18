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
        public DbSet<FactuurLijn> FactuurLijnen { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Factuur>()
                .Property(f => f.Prijs)
                .HasPrecision(18, 2);

           
            modelBuilder.Entity<Klant>()
                .Property(k => k.BtwPercentage)
                .HasPrecision(5, 2);

            
            modelBuilder.Entity<Klant>()
                .HasOne(k => k.Adres)
                .WithMany()
                .HasForeignKey(k => k.AdresId)
                .OnDelete(DeleteBehavior.SetNull);

           
            modelBuilder.Entity<Adres>()
                .HasOne(a => a.Land)
                .WithMany()
                .HasForeignKey(a => a.LandCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Factuur>().HasMany(a => a.FactuurLijnen)
                .WithOne(a => a.Factuur)
                .HasForeignKey(a => a.FactuurId)
                .OnDelete(DeleteBehavior.Restrict);


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

            modelBuilder.Entity<Land>().HasData(
                    new Land { LandCode = "BE", LandNaam = "België" },
                    new Land { LandCode = "FR", LandNaam = "Frankrijk" },
                    new Land { LandCode = "NL", LandNaam = "Nederland" },
                    new Land { LandCode = "DE", LandNaam = "Duitsland" },
                    new Land { LandCode = "EN", LandNaam = "Engeland" }

                );
        }


    }
}
