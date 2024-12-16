using AutoWorkshopApi.Models;
using AutoWorkshopApi.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopApi.Repositories
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        private readonly AutoWorkshopContext _context;

        public JobRepository(AutoWorkshopContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Job>> GetJobsByClientIdAsync(int clientId)
        {
            return await _context.Jobs
                                 .Where(j => j.ClientId == clientId)
                                 .ToListAsync();
        }
    }
}
