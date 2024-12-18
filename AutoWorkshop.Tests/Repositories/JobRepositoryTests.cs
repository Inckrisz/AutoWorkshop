using AutoWorkshopApi.Models;
using AutoWorkshopApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoWorkshop.Shared.Enums;

namespace AutoWorkshopApi.Tests.Repositories
{
    public class JobRepositoryTests
    {
        private AutoWorkshopContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AutoWorkshopContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AutoWorkshopContext(options);
            context.Database.EnsureCreated();

            return context;
        }

        [Fact]
        public async Task GetJobsByClientIdAsync_ReturnsJobs_WhenClientHasJobs()
        {
          
            var context = GetInMemoryDbContext();
            var client = new Client
            {
                Name = "John Doe",
                Address = "123 Test St",
                Email = "john.doe@example.com"
            };

            var jobs = new List<Job>
            {
                new Job
                {
                    ClientId = 1,
                    LicensePlate = "ABC-123",
                    ManufactureYear = 2020,
                    Category = "Repair",
                    Description = "Engine issue",
                    Severity = 5,
                    Status = JobStatus.FelvettMunka
                },
                new Job
                {
                    ClientId = 1,
                    LicensePlate = "XYZ-789",
                    ManufactureYear = 2021,
                    Category = "Maintenance",
                    Description = "Oil change",
                    Severity = 3,
                    Status = JobStatus.ElvegzesAlatt
                }
            };

            context.Clients.Add(client);
            context.Jobs.AddRange(jobs);
            await context.SaveChangesAsync();

            var repository = new JobRepository(context);

           
            var result = await repository.GetJobsByClientIdAsync(client.ClientId);

           
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, j => j.LicensePlate == "ABC-123");
            Assert.Contains(result, j => j.LicensePlate == "XYZ-789");
        }

        [Fact]
        public async Task GetJobsByClientIdAsync_ReturnsEmpty_WhenClientHasNoJobs()
        {
           
            var context = GetInMemoryDbContext();
            var client = new Client
            {
                Name = "Jane Doe",
                Address = "456 Another St",
                Email = "jane.doe@example.com"
            };

            context.Clients.Add(client);
            await context.SaveChangesAsync();

            var repository = new JobRepository(context);

           
            var result = await repository.GetJobsByClientIdAsync(client.ClientId);

          
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetJobsByClientIdAsync_ReturnsEmpty_WhenClientDoesNotExist()
        {
            
            var context = GetInMemoryDbContext();
            var repository = new JobRepository(context);

            
            var result = await repository.GetJobsByClientIdAsync(999);

            
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
