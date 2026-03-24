using CRM.Models;
using System.Net;
using System.Net.Http.Json;

namespace CRMProject.Client.Services
{
    public class FactuurService
    {
        private readonly HttpClient httpClient;
        public FactuurService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<List<Factuur>> GetFacturenAsync()
        {
            var response = await httpClient.GetAsync("Factuur");
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await
                        response.Content.ReadFromJsonAsync<List<Factuur>>();
                default:
                    return null;
            }
        }

        public async Task<List<BTWPercentage>> GetPercentagesAsync()
        {
            var response = await httpClient.GetAsync("Factuur/percentages");
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await
                        response.Content.ReadFromJsonAsync<List<BTWPercentage>>();
                default:
                    return null;
            }
        }

        public async Task<Factuur?> GetFactuurAsync(int id)
        {
            var response = await httpClient.GetAsync($"Factuur/{id}");
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await
                        response.Content.ReadFromJsonAsync<Factuur>();
                default:
                    return null;
            }
        }
        public async Task<Factuur?> UpdateFactuur(Factuur factuur)
        {
            HttpResponseMessage message = await httpClient.PutAsJsonAsync<Factuur>(
            $"factuur/{factuur.Id}", factuur);
            if (message.IsSuccessStatusCode)
                return factuur;
            else
                return null;
        }

        public async Task<Factuur?> AddFactuur(Factuur factuur)
        {
            HttpResponseMessage message = await httpClient.PostAsJsonAsync<Factuur>(
            $"factuur", factuur);
            if (message.IsSuccessStatusCode)
                return await message.Content.ReadFromJsonAsync<Factuur>();
            else
                return null;
        }

        public async Task DeleteFactuur(int id)
        {
            await httpClient.DeleteAsync($"factuur/{id}");
        }
    }
}
