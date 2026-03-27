using CRM.Models;
using CRM.API.Models;
using Microsoft.EntityFrameworkCore;
using CRM.Models.Enums;
namespace CRM.API.Repo
{
    public class SQLKlantenRepository : IKlantenRepository
    {
        private KlantenDbContext _context { get; set; }
        public SQLKlantenRepository(KlantenDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Klant>> GetKlantenAsync()
        {
            return await _context.Klanten.Include(klant=>klant.Adres).OrderBy(klant=>klant.Voornaam).ToListAsync();
        }

        public async Task<Klant?> GetKlantAsync(int id)
        {
            return await _context.Klanten
                .Include(klant => klant.Adres).ThenInclude(adres=>adres.Land)
                .Include(klant=>klant.Facturen)
                .FirstOrDefaultAsync(klant => klant.Id == id);
        }

        public async Task<Klant> AddKlantAsync(Klant klant)
        {
            if (klant.Adres != null)
            {
                if (klant.Adres.Land != null && !string.IsNullOrEmpty(klant.Adres.Land.LandCode))
                {
                    var bestaandLand = await _context.Landen
                        .FirstOrDefaultAsync(l => l.LandCode == klant.Adres.Land.LandCode);

                    if (bestaandLand != null)
                    {
                        klant.Adres.Land = bestaandLand;
                    }

                    klant.Adres.LandCode = klant.Adres.Land.LandCode;
                }
                klant.Adres.Land = null;
            }

            var result = await _context.Klanten.AddAsync(klant);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Klant> UpdateKlantAsync(Klant klant)
        {
            var result = await _context.Klanten
                .Include(k => k.Adres)
                .FirstOrDefaultAsync(k => k.Id == klant.Id);

            if (result != null)
            {
                _context.Entry(result).CurrentValues.SetValues(klant);

                if (klant.Adres != null)
                {
                    if (result.Adres != null)
                    {
                        result.Adres.Straat = klant.Adres.Straat;
                        result.Adres.HuisNummer = klant.Adres.HuisNummer;
                        result.Adres.BusNummer = klant.Adres.BusNummer;
                        result.Adres.Postcode = klant.Adres.Postcode;
                        result.Adres.Stad = klant.Adres.Stad;
                        result.Adres.Provincie = klant.Adres.Provincie;
                        result.Adres.LandCode = klant.Adres.LandCode;
                    }
                    else
                    {
                        result.Adres = klant.Adres;
                        result.Adres.Land = null;
                    }
                }
                else
                {
                    result.Adres = null;
                    result.AdresId = null;
                }

                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Klant> DeleteKlant(int KlantId)
        {
            var result = await _context.Klanten
                .Include(k => k.Facturen).ThenInclude(f => f.FactuurLijnen)
                .Include(k => k.Adres)
                .FirstOrDefaultAsync(c => c.Id == KlantId);
            if (result != null)
            {
                if (result.Facturen != null && result.Facturen.Any())
                {
                    foreach (var factuur in result.Facturen)
                    {
                        if (factuur.FactuurLijnen != null)
                        {
                            _context.FactuurLijnen.RemoveRange(factuur.FactuurLijnen);
                        }
                    }
                    _context.Facturen.RemoveRange(result.Facturen);
                }

                if (result.Adres != null)
                {
                    _context.Adressen.Remove(result.Adres);
                }

                _context.Klanten.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task Reset()
        {
            _context.FactuurLijnen.RemoveRange(_context.FactuurLijnen);
            _context.Facturen.RemoveRange(_context.Facturen);
            _context.Klanten.RemoveRange(_context.Klanten);
            _context.Adressen.RemoveRange(_context.Adressen);
            _context.GebruikersAccounts.RemoveRange(_context.GebruikersAccounts);
            _context.BTWPercentages.RemoveRange(_context.BTWPercentages);
            _context.Landen.RemoveRange(_context.Landen);

            await _context.SaveChangesAsync();

            await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('GebruikersAccounts', RESEED, 0)");
            await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('BTWPercentages', RESEED, 0)");

            _context.GebruikersAccounts.AddRange(
                new GebruikersAccount
                {
                    Email = "admin@admin.com",
                    Wachtwoord = "$2a$11$PE6KLR6iBRArcrrmg5Q3I.CeBU6YbTscN/nelbDhmhOchiDmqECaq",
                    SecurityLevel = SecurityLevel.Admin
                },
                new GebruikersAccount
                {
                    Email = "owner@owner.com",
                    Wachtwoord = "$2a$11$ETni2NLh0lIWizHEYV5k4OTSD5vSoQZXs5/ml1Cxz3.iv/m1eJ9zq",
                    SecurityLevel = SecurityLevel.Owner
                },
                new GebruikersAccount
                {
                    Email = "user@user.com",
                    Wachtwoord = "$2a$11$hO0heENjo4YZyHoPafKnzOP4sCXhAqDKmF4WBUtCSXizMM.UW96/m",
                    SecurityLevel = SecurityLevel.User
                }
            );

            _context.Landen.AddRange(
                new Land { LandCode = "BE", LandNaam = "België" },
                new Land { LandCode = "FR", LandNaam = "Frankrijk" },
                new Land { LandCode = "NL", LandNaam = "Nederland" },
                new Land { LandCode = "DE", LandNaam = "Duitsland" },
                new Land { LandCode = "EN", LandNaam = "Engeland" }
            );

            _context.BTWPercentages.AddRange(
                new BTWPercentage { Percentage = 6 },
                new BTWPercentage { Percentage = 12 },
                new BTWPercentage { Percentage = 21 }
            );

            await _context.SaveChangesAsync();
        }
    }
}
