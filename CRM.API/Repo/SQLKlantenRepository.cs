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
                        result.Adres.Gemeente = klant.Adres.Gemeente;
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
                .FirstOrDefaultAsync(c => c.Id == KlantId);
            if (result != null)
            {
                _context.Klanten.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
