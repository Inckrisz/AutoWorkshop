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
        // Arrange: Set up an in-memory database
        var options = GetInMemoryDbOptions("AutoWorkshopDb");

        // Create a new context with in-memory database
        using (var context = new AutoWorkshopContext(options))
        {
            // Create a client and a job associated with that client
            var client = new Client { Name = "John Doe", Email = "john@example.com", Address="Asd" };
            var job = new Job { Description = "Motor javítás", Category="Motor", ClientId=1, LicensePlate="XXX-234", ManufactureYear=2010, Severity = 10, Status = JobStatus.Befejezett };

            context.Clients.Add(client);
            context.Jobs.Add(job);

            await context.SaveChangesAsync(); // Save changes to the database
        }

        // Act: Now test the cascade delete behavior
        using (var context = new AutoWorkshopContext(options))
        {
            // Find the client and related job from the database
            var clientFromDb = await context.Clients
                .Include(c => c.Jobs)
                .FirstOrDefaultAsync(c => c.Name == "John Doe");

            Assert.NotNull(clientFromDb); // Assert client exists
            Assert.Single(clientFromDb.Jobs); // Assert that there is one job for this client

            // Delete the client, which should cascade delete the associated job
            context.Clients.Remove(clientFromDb);
            await context.SaveChangesAsync();

            // Act: Ensure the job has been deleted
            var jobFromDb = await context.Jobs
                .FirstOrDefaultAsync(j => j.Description == "Engine Repair");

            // Assert: Job should be deleted due to cascade delete
            Assert.Null(jobFromDb); // The job should no longer exist in the database
        }
    }
}
