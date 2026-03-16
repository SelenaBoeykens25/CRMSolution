using CRM.Models;
using CRM.API.Models;
using Microsoft.EntityFrameworkCore;
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
            return await _context.Klanten.ToListAsync();
        }

        public async Task<Klant?> GetKlantAsync(int id)
        {
            return await _context.Klanten
                .Include(klant => klant.Adres).ThenInclude(adres=>adres.Land)
                .Include(klant=>klant.Facturen)
                .FirstOrDefaultAsync(klant => klant.Id == id);
        }

        public async Task<Klant?> GetKlantAsync(string naam)
        {
            return await _context.Klanten
                .FirstOrDefaultAsync(c => c.Naam.ToLower() == naam.ToLower());
        }

        public async Task<Klant> AddKlantAsync(Klant klant)
        {
            if (klant.Adres != null)
            {
                // Handle new land creation if provided
                if (klant.Adres.Land != null && !string.IsNullOrEmpty(klant.Adres.Land.LandCode))
                {
                    var existingLand = await _context.Landen
                        .FirstOrDefaultAsync(l => l.LandCode == klant.Adres.Land.LandCode);

                    if (existingLand != null)
                    {
                        klant.Adres.Land = existingLand;
                    }

                    klant.Adres.LandCode = klant.Adres.Land.LandCode;
                }

                // Clear Land navigation property to avoid tracking issues
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
                result.Voornaam = klant.Voornaam;
                result.Naam = klant.Naam;
                result.Aanspreking = klant.Aanspreking;
                result.BtwPercentage = klant.BtwPercentage;
                result.TelefoonNummer = klant.TelefoonNummer;
                result.EmailAdres = klant.EmailAdres;
                result.GeboorteDatum = klant.GeboorteDatum;

                // Handle address updates
                if (klant.Adres != null)
                {
                    if (result.Adres != null)
                    {
                        // Update existing address
                        result.Adres.Straat = klant.Adres.Straat;
                        result.Adres.HuisNummer = klant.Adres.HuisNummer;
                        result.Adres.BusNummer = klant.Adres.BusNummer;
                        result.Adres.Postcode = klant.Adres.Postcode;
                        result.Adres.Stad = klant.Adres.Stad;
                        result.Adres.Gemeente = klant.Adres.Gemeente;
                        result.Adres.LandCode = klant.Adres.LandCode;
                    }
                    else
                    {
                        // Add new address
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
    }
}
