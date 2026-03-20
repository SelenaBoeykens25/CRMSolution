using CRM.Models;
using System.Net;
using System.Net.Http.Json;

namespace CRMProject.Client.Services
{
    public class AccountService
    {
        private readonly HttpClient httpClient;
        public AccountService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<GebruikersAccount?> LoginAsync(string email, string wachtwoord)
        {
            try
            {
                var response = await httpClient.GetAsync($"account/{email}/{wachtwoord}");
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return await
                            response.Content.ReadFromJsonAsync<GebruikersAccount>();
                    default:
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> BestaatAlAsync(string email)
        {
            var response = await httpClient.GetAsync($"/account/{email}");
            return response.IsSuccessStatusCode && await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<GebruikersAccount?> AddAccountAsync(GebruikersAccount account)
        {
            var response = await httpClient.PostAsJsonAsync("/account", account);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GebruikersAccount>();
            }
            return null;
        }
    }
}
