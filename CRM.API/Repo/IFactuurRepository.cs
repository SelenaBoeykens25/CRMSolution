using CRM.Models;

namespace CRM.API.Repo
{
    public interface IFactuurRepository
    {
        Task<Factuur?> GetFactuurAsync(int id);
        Task<IEnumerable<Factuur>> GetFacturenAsync();
        Task<IEnumerable<Factuur>> GetFacturenVanAsync(int klantId);
        Task<Factuur> AddFactuurAsync(Factuur factuur);
        Task<Factuur> DeleteFactuurAsync(int id);
        Task<FactuurLijn?> GetFactuurLijnAsync(int id);
        Task<IEnumerable<FactuurLijn>> GetFactuurLijnenVanAsync(int factuurId);
        Task<FactuurLijn> AddFactuurLijnAsync(FactuurLijn factuurLijn);
        Task<FactuurLijn> DeleteFactuurLijn(int id);
        Task<Factuur> UpdateFactuurAsync(Factuur factuur);
        Task<FactuurLijn> UpdateFactuurLijnAsync(FactuurLijn factuurLijn);
    }
}
