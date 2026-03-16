using CRM.Models;
namespace CRM.API.Repo
{
    public interface IKlantenRepository
    {
        Task<IEnumerable<Klant>> GetKlantenAsync();
        Task<Klant?> GetKlantAsync(int id);
        Task<Klant?> GetKlantAsync(string naam);
        Task<Klant> AddKlantAsync(Klant klant);
        Task<Klant> UpdateKlantAsync(Klant klant);
    }
}
