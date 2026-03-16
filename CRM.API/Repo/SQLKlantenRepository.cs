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

        public Task<Klant?> GetKlantAsync(string naam)
        {
            throw new NotImplementedException();
        }
    }
}
