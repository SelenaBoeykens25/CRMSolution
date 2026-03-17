using CRM.Models;
using System.Net;
using System.Net.Http.Json;

namespace CRMProject.Client.Services
{
    public class KlantenService
    {
        private readonly HttpClient httpClient;
        public KlantenService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<Klant>> GetKlantenAsync()
        {
            var response = await httpClient.GetAsync("klant");
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await
                        response.Content.ReadFromJsonAsync<List<Klant>>();
                default:
                    return null;
            }
        }

        public async Task<Klant?> GetKlantAsync(int id)
        {
            var response = await httpClient.GetAsync($"klant/{id}");
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await
                        response.Content.ReadFromJsonAsync<Klant>();
                default:
                    return null;
            }
        }
        public async Task<Klant?> UpdateKlant(Klant klant)
        {
            HttpResponseMessage message = await httpClient.PutAsJsonAsync<Klant>(
            $"klant/{klant.Id}", klant);
            if (message.IsSuccessStatusCode)
                return klant;
            else
                return null;
        }

        public async Task<Klant?> AddKlant(Klant klant)
        {
            HttpResponseMessage message = await httpClient.PostAsJsonAsync<Klant>(
            $"klant", klant);
            if (message.IsSuccessStatusCode)
                return klant;
            else
                return null;
        }

        public async Task DeleteKlant(int id)
        {
            await httpClient.DeleteAsync($"klant/{id}");
        }
    }
}
