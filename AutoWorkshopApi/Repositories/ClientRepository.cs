using AutoWorkshopApi.Models;
using AutoWorkshopApi.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopApi.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly AutoWorkshopContext _context;

        public ClientRepository(AutoWorkshopContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Client?> GetClientWithJobsAsync(int id)
        {
            return await _context.Clients
                                 .Include(c => c.Jobs)
                                 .FirstOrDefaultAsync(c => c.ClientId == id);
        }
    }
}
