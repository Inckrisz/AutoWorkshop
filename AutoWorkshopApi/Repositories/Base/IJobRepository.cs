using AutoWorkshopApi.Models;

namespace AutoWorkshopApi.Repositories.Base
{
    public interface IJobRepository : IRepository<Job>
    {
       
        Task<IEnumerable<Job>> GetJobsByClientIdAsync(int clientId);
    }
}
