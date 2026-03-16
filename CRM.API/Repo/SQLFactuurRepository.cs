using CRM.API.Models;

namespace CRM.API.Repo
{
    public class SQLFactuurRepository
    {
        private KlantenDbContext _context { get; set; }
        public SQLFactuurRepository(KlantenDbContext context)
        {
            _context = context;
        }
    }
}
