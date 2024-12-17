using System.Net.Http.Json;
using AutoWorkshop.AutoWorkshopClient.Services;
using AutoWorkshop.Shared.DTOs;

namespace AutoWorkshop.AutoWorkshopClient.Services;

public class ClientService : IClientService
{
    private readonly HttpClient _httpClient;

    public ClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ClientDTO>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<ClientDTO>>("clients");
    }

    public async Task<ClientDTO> GetByIdAsync(int clientId)
    {
        return await _httpClient.GetFromJsonAsync<ClientDTO>($"clients/{clientId}");
    }

    public async Task<ClientWithJobsDTO> GetWithJobsAsync(int clientId)
    {
        return await _httpClient.GetFromJsonAsync<ClientWithJobsDTO>($"clients/{clientId}/jobs");
    }

    public async Task AddAsync(ClientDTO client)
    {
        await _httpClient.PostAsJsonAsync("clients", client);
    }

    public async Task UpdateAsync(ClientDTO client)
    {
        await _httpClient.PutAsJsonAsync($"clients/{client.ClientId}", client);
    }

    public async Task DeleteAsync(int clientId)
    {
        await _httpClient.DeleteAsync($"clients/{clientId}");
    }
}
