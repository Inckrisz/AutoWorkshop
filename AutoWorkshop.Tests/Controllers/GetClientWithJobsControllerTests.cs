using AutoWorkshop.Shared.DTOs;
using AutoWorkshop.Shared.Enums;
using AutoWorkshopApi.Controllers;
using AutoWorkshopApi.Models;
using AutoWorkshopApi.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class ClientsWithJobsControllerTests
{
    private readonly ClientsWithJobsController _controller;
    private readonly Mock<IClientRepository> _clientRepositoryMock;
    private readonly Mock<JobEstimationService> _jobEstimationServiceMock;

    public ClientsWithJobsControllerTests()
    {
        _clientRepositoryMock = new Mock<IClientRepository>();
        _jobEstimationServiceMock = new Mock<JobEstimationService>();
        _controller = new ClientsWithJobsController(_clientRepositoryMock.Object, _jobEstimationServiceMock.Object);
    }

    [Fact]
    public async Task GetClientWithJobs_ShouldReturnNotFound_WhenClientDoesNotExist()
    {
       
        var clientId = 1;
        _clientRepositoryMock.Setup(repo => repo.GetClientWithJobsAsync(clientId)).ReturnsAsync((Client)null);

        
        var result = await _controller.GetClientWithJobs(clientId);

      
        var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task GetClientWithJobs_ShouldReturnOk_WhenClientWithJobsExists()
    {
        
        var clientId = 1;
        var client = new Client
        {
            ClientId = clientId,
            Name = "John Doe",
            Address = "123 Main St",
            Email = "john.doe@example.com",
            Jobs = new List<Job>
        {
            new Job
            {
                JobId = 1,
                ClientId = clientId,
                LicensePlate = "XYZ123",
                ManufactureYear = 2020,
                Category = "Repair",
                Description = "Brake Repair",
                Severity = 3,
                Status = JobStatus.FelvettMunka
            }
        }
        };

        _clientRepositoryMock.Setup(repo => repo.GetClientWithJobsAsync(clientId)).ReturnsAsync(client);
        _jobEstimationServiceMock.Setup(service => service.CalculateEstimatedHours(It.IsAny<Job>())).Returns(100.0);

       
        var result = await _controller.GetClientWithJobs(clientId);

        
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var clientWithJobsDto = Assert.IsType<ClientWithJobsDTO>(okResult.Value);

        Assert.Equal(clientId, clientWithJobsDto.ClientId);
        Assert.Equal(client.Name, clientWithJobsDto.Name);
        Assert.Equal(client.Address, clientWithJobsDto.Address);
        Assert.Equal(client.Email, clientWithJobsDto.Email);
        Assert.Single(clientWithJobsDto.Jobs);

        var jobDto = clientWithJobsDto.Jobs.First(); 
        Assert.Equal(client.Jobs.First().JobId, jobDto.JobId); 
        Assert.Equal(client.Jobs.First().LicensePlate, jobDto.LicensePlate); 
        Assert.Equal(100.0, jobDto.EstimatedCost); 
    }


    [Fact]
    public async Task GetClientWithJobs_ShouldReturnOk_WhenClientHasNoJobs()
    {
        
        var clientId = 2;
        var client = new Client
        {
            ClientId = clientId,
            Name = "Jane Smith",
            Address = "456 Oak St",
            Email = "jane.smith@example.com",
            Jobs = new List<Job>() 
        };

        _clientRepositoryMock.Setup(repo => repo.GetClientWithJobsAsync(clientId)).ReturnsAsync(client);

       
        var result = await _controller.GetClientWithJobs(clientId);

       
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var clientWithJobsDto = Assert.IsType<ClientWithJobsDTO>(okResult.Value);

        Assert.Equal(clientId, clientWithJobsDto.ClientId);
        Assert.Equal(client.Name, clientWithJobsDto.Name);
        Assert.Equal(client.Address, clientWithJobsDto.Address);
        Assert.Equal(client.Email, clientWithJobsDto.Email);
        Assert.Empty(clientWithJobsDto.Jobs); 
    }
}
