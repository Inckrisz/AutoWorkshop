using AutoWorkshop.Shared.DTOs;

namespace AutoWorkshop.AutoWorkshopClient.Services;

public interface IClientService
{
    Task<List<ClientDTO>> GetAllAsync();
    Task<ClientDTO> GetByIdAsync(int clientId);
    Task<ClientWithJobsDTO> GetWithJobsAsync(int clientId);
    Task AddAsync(ClientDTO client);
    Task UpdateAsync(ClientDTO client);
    Task DeleteAsync(int clientId);
}
