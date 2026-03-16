using CRM.Models;

namespace CRM.API.Repo
{
    public interface ILandRepository
    {
        Task<IEnumerable<Land>> GetLandenAsync();
        Task<Land> AddLandAsync(Land land);
        Task<Land?> GetLandAsync(string landcode);
    }
}
