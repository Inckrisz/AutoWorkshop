namespace AutoWorkshop.AutoWorkshopClient.Services
{
    using AutoWorkshop.Shared.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IClientsWithJobsService
    {
        Task<List<ClientWithJobsDTO>> GetAllWithJobsAsync();
        Task<ClientWithJobsDTO> GetClientWithJobsAsync(int clientId);
    }
}
