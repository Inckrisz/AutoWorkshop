using AutoWorkshopApi.Models;

namespace AutoWorkshopApi.Repositories.Base
{
    public interface IClientRepository : IRepository<Client>
    {
        // Define any additional methods specific to Client
        Task<Client?> GetClientWithJobsAsync(int id);
    }
}
