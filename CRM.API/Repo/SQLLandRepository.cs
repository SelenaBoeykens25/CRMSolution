using CRM.API.Models;
using CRM.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.API.Repo
{
    public class SQLLandRepository : ILandRepository
    {
        private KlantenDbContext _context { get; set; }
        public SQLLandRepository(KlantenDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Land>> GetLandenAsync()
        {
            return await _context.Landen.ToListAsync();
        }

        public async Task<Land> AddLandAsync(Land land)
        {
            var result = await _context.Landen.AddAsync(land);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Land?> GetLandAsync(string landcode)
        {
            return await _context.Landen.FirstOrDefaultAsync(land => land.LandCode.ToLower() == landcode.ToLower());
        }
    }
}
