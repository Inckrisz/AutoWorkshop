using AutoWorkshopApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AutoWorkshopApi.Models;

namespace AutoWorkshopApi.Tests.Repositories
{
    public class RepositoryTests
    {
        private DbContextOptions<AutoWorkshopContext> CreateInMemoryOptions()
        {
            return new DbContextOptionsBuilder<AutoWorkshopContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEntities()
        {
           
            var options = CreateInMemoryOptions();
            using var context = new AutoWorkshopContext(options);

            var clients = new List<Client>
            {
                new Client { Name = "John Doe", Address = "123 Test St", Email = "john.doe@example.com" },
                new Client { Name = "Jane Doe", Address = "456 Another St", Email = "jane.doe@example.com" }
            };

            context.Clients.AddRange(clients);
            await context.SaveChangesAsync();

            var repository = new Repository<Client>(context);

            
            var result = await repository.GetAllAsync();

         
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsEntity_WhenEntityExists()
        {
            
            var options = CreateInMemoryOptions();
            using var context = new AutoWorkshopContext(options);

            var client = new Client { Name = "John Doe", Address = "123 Test St", Email = "john.doe@example.com" };

            context.Clients.Add(client);
            await context.SaveChangesAsync();

            var repository = new Repository<Client>(context);

           
            var result = await repository.GetByIdAsync(client.ClientId);

           
            Assert.NotNull(result);
            Assert.Equal(client.ClientId, result?.ClientId);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenEntityDoesNotExist()
        {
            
            var options = CreateInMemoryOptions();
            using var context = new AutoWorkshopContext(options);

            var repository = new Repository<Client>(context);

            
            var result = await repository.GetByIdAsync(999);

            
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_AddsEntityToDatabase()
        {
           
            var options = CreateInMemoryOptions();
            using var context = new AutoWorkshopContext(options);

            var client = new Client { Name = "John Doe", Address = "123 Test St", Email = "john.doe@example.com" };

            var repository = new Repository<Client>(context);

           
            await repository.AddAsync(client);
            await repository.SaveChangesAsync();

           
            var savedClient = await context.Clients.FindAsync(client.ClientId);
            Assert.NotNull(savedClient);
            Assert.Equal(client.Name, savedClient.Name);
        }

        [Fact]
        public async Task Update_UpdatesEntityInDatabase()
        {
            
            var options = CreateInMemoryOptions();
            using var context = new AutoWorkshopContext(options);

            var client = new Client { Name = "John Doe", Address = "123 Test St", Email = "john.doe@example.com" };

            context.Clients.Add(client);
            await context.SaveChangesAsync();

            var repository = new Repository<Client>(context);

           
            client.Name = "John Updated";
            repository.Update(client);
            await repository.SaveChangesAsync();

            var updatedClient = await context.Clients.FindAsync(client.ClientId);
            Assert.NotNull(updatedClient);
            Assert.Equal("John Updated", updatedClient.Name);
        }

        [Fact]
        public async Task Delete_RemovesEntityFromDatabase()
        {
           
            var options = CreateInMemoryOptions();
            using var context = new AutoWorkshopContext(options);

            var client = new Client { Name = "John Doe", Address = "123 Test St", Email = "john.doe@example.com" };

            context.Clients.Add(client);
            await context.SaveChangesAsync();

            var repository = new Repository<Client>(context);

           
            repository.Delete(client);
            await repository.SaveChangesAsync();

            
            var deletedClient = await context.Clients.FindAsync(client.ClientId);
            Assert.Null(deletedClient);
        }

        [Fact]
        public async Task FindAsync_ReturnsMatchingEntities()
        {
            
            var options = CreateInMemoryOptions();
            using var context = new AutoWorkshopContext(options);

            var clients = new List<Client>
            {
                new Client { Name = "John Doe", Address = "123 Test St", Email = "john.doe@example.com" },
                new Client { Name = "Jane Doe", Address = "456 Another St", Email = "jane.doe@example.com" }
            };

            context.Clients.AddRange(clients);
            await context.SaveChangesAsync();

            var repository = new Repository<Client>(context);

           
            var result = await repository.FindAsync(c => c.Name.Contains("Doe"));

           
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
