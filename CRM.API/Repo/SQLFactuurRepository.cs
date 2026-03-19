using CRM.API.Models;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace CRM.API.Repo
{
    public class SQLFactuurRepository : IFactuurRepository
    {
        private KlantenDbContext _context { get; set; }
        public SQLFactuurRepository(KlantenDbContext context)
        {
            _context = context;
        }

        public async Task<Factuur?> GetFactuurAsync(int id)
        {
            return await _context.Facturen
                .Include(factuur => factuur.FactuurLijnen)
                .Include(factuur => factuur.Klant).ThenInclude(klant=>klant.Adres).ThenInclude(adres=>adres.Land)
                .FirstOrDefaultAsync(factuur => factuur.Id == id);
        }

        public async Task<IEnumerable<Factuur>> GetFacturenAsync()
        {
            return await _context.Facturen
                .Include(factuur => factuur.FactuurLijnen)
                .Include(factuur => factuur.Klant)
                .OrderBy(factuur => factuur.KlantId).ThenBy(factuur => factuur.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Factuur>> GetFacturenVanAsync(int klantId)
        {
            return await _context.Facturen
                .Include(factuur => factuur.FactuurLijnen)
                .Include(factuur => factuur.Klant)
                .Where(factuur => factuur.KlantId == klantId)
                .ToListAsync();
        }

        public async Task<Factuur> AddFactuurAsync(Factuur factuur)
        {
            if (factuur.FactuurLijnen == null)
            {
                factuur.FactuurLijnen = new List<FactuurLijn>();
            }

            var result = await _context.Facturen.AddAsync(factuur);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Factuur> DeleteFactuurAsync(int id)
        {
            var result = await _context.Facturen
                .Include(factuur => factuur.FactuurLijnen)
                .FirstOrDefaultAsync(factuur => factuur.Id == id);
            if (result != null)
            {
                _context.Facturen.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<FactuurLijn?> GetFactuurLijnAsync(int id)
        {
            return await _context.FactuurLijnen.FindAsync(id);
        }

        public async Task<IEnumerable<FactuurLijn>> GetFactuurLijnenVanAsync(int id)
        {
            return await _context.FactuurLijnen.Where(fl => fl.FactuurId == id)
                .ToListAsync();
        }

        public async Task<FactuurLijn> AddFactuurLijnAsync(FactuurLijn factuurLijn)
        {
            var result = await _context.FactuurLijnen.AddAsync(factuurLijn);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<FactuurLijn> DeleteFactuurLijn(int id)
        {
            var result = await _context.FactuurLijnen
                .FirstOrDefaultAsync(fl => fl.Id == id);
            if (result != null)
            {
                _context.FactuurLijnen.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Factuur> UpdateFactuurAsync(Factuur factuur)
        {
            var existingFactuur = await _context.Facturen
                .Include(f => f.FactuurLijnen)
                .FirstOrDefaultAsync(f => f.Id == factuur.Id);

            if (existingFactuur == null)
                return null;

            _context.Entry(existingFactuur).CurrentValues.SetValues(factuur);

            var existingFactuurLijnenIds = existingFactuur.FactuurLijnen.Select(fl => fl.Id).ToList();
            var updatedFactuurLijnenIds = factuur.FactuurLijnen.Where(fl => fl.Id != 0).Select(fl => fl.Id).ToList();

            var factuurLijnenToDelete = existingFactuur.FactuurLijnen
                .Where(fl => !updatedFactuurLijnenIds.Contains(fl.Id))
                .ToList();
            foreach (var lijnToDelete in factuurLijnenToDelete)
            {
                _context.FactuurLijnen.Remove(lijnToDelete);
            }

            foreach (var factuurLijn in factuur.FactuurLijnen)
            {
                factuurLijn.FactuurId = factuur.Id;

                if (factuurLijn.Id == 0)
                {
                    existingFactuur.FactuurLijnen.Add(factuurLijn);
                }
                else
                {
                    var existingLijn = existingFactuur.FactuurLijnen.FirstOrDefault(fl => fl.Id == factuurLijn.Id);
                    if (existingLijn != null)
                    {
                        _context.Entry(existingLijn).CurrentValues.SetValues(factuurLijn);
                    }
                }
            }

            await _context.SaveChangesAsync();
            return existingFactuur;
        }

        public async Task<FactuurLijn> UpdateFactuurLijnAsync(FactuurLijn factuurLijn)
        {
            var result = await _context.FactuurLijnen.FirstOrDefaultAsync(fl => fl.Id == factuurLijn.Id);
            if (result != null)
            {
                _context.Entry(result).CurrentValues.SetValues(factuurLijn);
            }
            else
            {
                result = await AddFactuurLijnAsync(factuurLijn);
            }
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
