using CRM.Models;

namespace CRM.API.Repo
{
    public interface IAccountRepository
    {
        Task<GebruikersAccount?> GetAccountAsync(string email, string wachtwoord);
        Task<bool> BestaatAlAsync(string email);
        Task<GebruikersAccount> AddAccountAsync(GebruikersAccount account);

    }
}
