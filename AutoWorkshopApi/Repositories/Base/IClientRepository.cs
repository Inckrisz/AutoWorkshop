using AutoWorkshopApi.Models;

namespace AutoWorkshopApi.Repositories.Base
{
    public interface IClientRepository : IRepository<Client>
    {
       
        Task<Client> GetClientWithJobsAsync(int id);
    }
}
