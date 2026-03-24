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
        public DbSet<BTWPercentage> BTWPercentages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GebruikersAccount>()
                .HasIndex(g => g.Email)
                .IsUnique();

            modelBuilder.Entity<Factuur>()
                .Property(f => f.Prijs)
                .HasPrecision(18, 2);

            
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

            //heefd voorcomputeerde hashes, anders veranderen ze steeds
            modelBuilder.Entity<GebruikersAccount>().HasData(
                new GebruikersAccount
                {
                    Id = 1,
                    Email = "admin@admin.com",
                    Wachtwoord = "$2a$11$PE6KLR6iBRArcrrmg5Q3I.CeBU6YbTscN/nelbDhmhOchiDmqECaq",
                    SecurityLevel = SecurityLevel.Admin
                },
                new GebruikersAccount
                {
                    Id = 2,
                    Email = "owner@owner.com",
                    Wachtwoord = "$2a$11$ETni2NLh0lIWizHEYV5k4OTSD5vSoQZXs5/ml1Cxz3.iv/m1eJ9zq",
                    SecurityLevel = SecurityLevel.Owner
                },
                new GebruikersAccount
                {
                    Id = 3,
                    Email = "user@user.com",
                    Wachtwoord = "$2a$11$XJbsMCPJ4CAJMT0KD.0yLOlTGnhAn97IP.BLATYzBBdvV7W9LdhU2",
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
            modelBuilder.Entity<BTWPercentage>().HasData(
                new BTWPercentage { Id = 1, Percentage = 6 },
                new BTWPercentage { Id = 2, Percentage = 12 },
                new BTWPercentage { Id = 3, Percentage = 21 }
                );

        }
    }
}
