using CRM.API.Models;
using CRM.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.API.Repo
{
    public class SQLAccountRepository : IAccountRepository
    {
        private KlantenDbContext _context { get; set; }
        public SQLAccountRepository(KlantenDbContext context)
        {
            _context = context;
        }

        public async Task<GebruikersAccount> AddAccountAsync(GebruikersAccount account)
        {
            account.Wachtwoord = BCrypt.Net.BCrypt.HashPassword(account.Wachtwoord);
    
            var result = await _context.GebruikersAccounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<GebruikersAccount?> GetAccountAsync(string email, string wachtwoord)
        {
            var account = await _context.GebruikersAccounts
                .FirstOrDefaultAsync(a => a.Email == email);
            
            if (account != null && BCrypt.Net.BCrypt.Verify(wachtwoord, account.Wachtwoord))
            {
                return account;
            }
            
            return null;
        }
        public async Task<bool> BestaatAlAsync(string email)
        {
            var account = await _context.GebruikersAccounts
                .FirstOrDefaultAsync(account => account.Email.ToLower() == email.ToLower());
            return account != null;
        }
    }
}
