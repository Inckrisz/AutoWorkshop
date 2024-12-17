using AutoWorkshop.Shared.DTOs;
using AutoWorkshopApi.Controllers;
using AutoWorkshopApi.Models;
using AutoWorkshopApi.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;

public class ClientsControllerTests
{
    private readonly Mock<IClientRepository> _clientRepositoryMock;
    private readonly ClientsController _controller;

    public ClientsControllerTests()
    {
        _clientRepositoryMock = new Mock<IClientRepository>();
        _controller = new ClientsController(_clientRepositoryMock.Object);
    }

    [Fact]
    public async Task GetClients_ReturnsOkResult_WhenClientsExist()
    {
        // Arrange
        var clients = new List<Client>
    {
        new Client { ClientId = 1, Name = "John Doe", Address = "123 Main St", Email = "john.doe@example.com" },
        new Client { ClientId = 2, Name = "Jane Smith", Address = "456 Oak St", Email = "jane.smith@example.com" }
    };
        _clientRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(clients);

        // Act
        var result = await _controller.GetClients();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var clientDtos = Assert.IsType<List<ClientDTO>>(okResult.Value);
        Assert.Equal(2, clientDtos.Count);
    }


    [Fact]
    public async Task GetClient_ReturnsOkResult_WhenClientExists()
    {
        // Arrange
        var client = new Client { ClientId = 1, Name = "John Doe", Address = "123 Main St", Email = "john.doe@example.com" };
        _clientRepositoryMock.Setup(repo => repo.GetClientWithJobsAsync(1)).ReturnsAsync(client);

        // Act
        var result = await _controller.GetClient(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var clientDto = Assert.IsType<ClientDTO>(okResult.Value);
        Assert.Equal(client.ClientId, clientDto.ClientId);
    }

    [Fact]
    public async Task GetClient_ReturnsNotFound_WhenClientDoesNotExist()
    {
        // Arrange
        _clientRepositoryMock.Setup(repo => repo.GetClientWithJobsAsync(It.IsAny<int>())).ReturnsAsync((Client)null);

        // Act
        var result = await _controller.GetClient(999);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateClient_ReturnsCreatedAtAction_WhenClientIsValid()
    {
        // Arrange
        var clientDto = new ClientDTO
        {
            Name = "John Doe",
            Address = "123 Main St",
            Email = "john.doe@example.com"
        };
        var client = new Client { ClientId = 1, Name = clientDto.Name, Address = clientDto.Address, Email = clientDto.Email };

        _clientRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Client>())).Returns(Task.CompletedTask);
        _clientRepositoryMock.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.CreateClient(clientDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedClientDto = Assert.IsType<ClientDTO>(createdAtActionResult.Value);
        Assert.Equal(clientDto.Name, returnedClientDto.Name);
    }


    [Fact]
    public async Task CreateClient_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        var clientDto = new ClientDTO { Name = "", Address = "", Email = "" }; // Invalid data
        _controller.ModelState.AddModelError("Name", "Name is required");

        // Act
        var result = await _controller.CreateClient(clientDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);

        // Ensure that ModelState contains the error for "Name"
        var modelErrors = Assert.IsType<SerializableError>(badRequestResult.Value);
        var errorList = modelErrors["Name"] as IEnumerable<string>; // Retrieve the error for "Name"

        Assert.NotNull(errorList); // Ensure there are errors for "Name"
        Assert.Contains("Name is required", errorList); // Check that the error message is present
    }




    [Fact]
    public async Task UpdateClient_ReturnsNoContent_WhenClientIsValid()
    {
        // Arrange
        var clientDto = new ClientDTO { ClientId = 1, Name = "John Doe", Address = "123 Main St", Email = "john.doe@example.com" };
        var existingClient = new Client { ClientId = 1, Name = "Old Name", Address = "Old Address", Email = "old.email@example.com" };

        _clientRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingClient);
        _clientRepositoryMock.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateClient(1, clientDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateClient_ReturnsNotFound_WhenClientDoesNotExist()
    {
        // Arrange
        var clientDto = new ClientDTO { ClientId = 999, Name = "Nonexistent", Address = "No Address", Email = "no.email@example.com" };
        _clientRepositoryMock.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((Client)null);

        // Act
        var result = await _controller.UpdateClient(999, clientDto);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task DeleteClient_ReturnsNoContent_WhenClientIsDeleted()
    {
        // Arrange
        var client = new Client { ClientId = 1, Name = "John Doe", Address = "123 Main St", Email = "john.doe@example.com" };
        _clientRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(client);
        _clientRepositoryMock.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteClient(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteClient_ReturnsNotFound_WhenClientDoesNotExist()
    {
        // Arrange
        _clientRepositoryMock.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((Client)null);

        // Act
        var result = await _controller.DeleteClient(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }


}


