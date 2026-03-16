using CRM.Models;
using System.Net;
using System.Net.Http.Json;

namespace CRMProject.Client.Services
{
    public class LandService
    {
        private readonly HttpClient httpClient;
        public LandService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Land>> GetLandenAsync()
        {
            var response = await httpClient.GetAsync("land");
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await
                        response.Content.ReadFromJsonAsync<List<Land>>();
                default:
                    return null;
            }
        }

        public async Task<Land?> AddLand(Land land)
        {
            HttpResponseMessage message = await httpClient.PostAsJsonAsync<Land>(
            $"land", land);
            if (message.IsSuccessStatusCode)
                return land;
            else
                return null;
        }
    }
}
