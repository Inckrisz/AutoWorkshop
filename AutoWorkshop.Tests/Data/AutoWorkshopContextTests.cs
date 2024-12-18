using AutoWorkshop.Shared.Enums;
using AutoWorkshopApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class AutoWorkshopContextTests
{
    private DbContextOptions<AutoWorkshopContext> GetInMemoryDbOptions(string dbName)
    {
        return new DbContextOptionsBuilder<AutoWorkshopContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
    }

    [Fact]
    public async Task Test_Client_Job_Relationship_And_Cascade_Delete()
    {
        
        var options = GetInMemoryDbOptions("AutoWorkshopDb");

        
        using (var context = new AutoWorkshopContext(options))
        {
           
            var client = new Client { Name = "John Doe", Email = "john@example.com", Address="Asd" };
            var job = new Job { Description = "Motor javítás", Category="Motor", ClientId=1, LicensePlate="XXX-234", ManufactureYear=2010, Severity = 10, Status = JobStatus.Befejezett };

            context.Clients.Add(client);
            context.Jobs.Add(job);

            await context.SaveChangesAsync(); 
        }

        
        using (var context = new AutoWorkshopContext(options))
        {
            
            var clientFromDb = await context.Clients
                .Include(c => c.Jobs)
                .FirstOrDefaultAsync(c => c.Name == "John Doe");

            Assert.NotNull(clientFromDb);
            Assert.Single(clientFromDb.Jobs); 

            
            context.Clients.Remove(clientFromDb);
            await context.SaveChangesAsync();

            
            var jobFromDb = await context.Jobs
                .FirstOrDefaultAsync(j => j.Description == "Engine Repair");

            
            Assert.Null(jobFromDb); 
        }
    }
}
