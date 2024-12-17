using AutoWorkshop.Shared.Enums;
using AutoWorkshopApi.Models;
using AutoWorkshopApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace AutoWorkshopApi.Tests.Repositories
{
    public class ClientRepositoryTests
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
        public async Task GetClientWithJobsAsync_ReturnsClientWithJobs_WhenClientExists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var client = new Client
            {
                Name = "John Doe",
                Address = "123 Test St",
                Email = "john.doe@example.com",
                Jobs = new List<Job>
                {
                    new Job
                    {
                        LicensePlate = "ABC-123",
                        ManufactureYear = 2020,
                        Category = "Repair",
                        Description = "Engine issue",
                        Severity = 5,
                        Status = JobStatus.FelvettMunka
                    }
                }
            };
            context.Clients.Add(client);
            await context.SaveChangesAsync();

            var repository = new ClientRepository(context);

            // Act
            var result = await repository.GetClientWithJobsAsync(client.ClientId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(client.ClientId, result!.ClientId);
            Assert.Single(result.Jobs);
            Assert.Equal("ABC-123", result.Jobs.First().LicensePlate);
        }

        [Fact]
        public async Task GetClientWithJobsAsync_ReturnsNull_WhenClientDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new ClientRepository(context);

            // Act
            var result = await repository.GetClientWithJobsAsync(999);

            // Assert
            Assert.Null(result);
        }
    }
}
