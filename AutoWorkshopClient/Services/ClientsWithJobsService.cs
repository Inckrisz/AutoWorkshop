namespace AutoWorkshop.AutoWorkshopClient.Services
{
    using AutoWorkshop.Shared.DTOs;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class ClientsWithJobsService : IClientsWithJobsService
    {
        private readonly HttpClient _httpClient;

        public ClientsWithJobsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetches all clients with their jobs
        public async Task<List<ClientWithJobsDTO>> GetAllWithJobsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ClientWithJobsDTO>>("clientswjobs");
        }

        // Fetches a single client by ID along with their jobs
        public async Task<ClientWithJobsDTO> GetClientWithJobsAsync(int clientId)
        {
            return await _httpClient.GetFromJsonAsync<ClientWithJobsDTO>($"clientswjobs/{clientId}");
        }
    }
}
