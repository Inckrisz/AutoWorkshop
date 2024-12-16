using AutoWorkshopApi.Models;

namespace AutoWorkshopApi.Repositories.Base
{
    public interface IJobRepository : IRepository<Job>
    {
        // Define any additional methods specific to Job
        Task<IEnumerable<Job>> GetJobsByClientIdAsync(int clientId);
    }
}
