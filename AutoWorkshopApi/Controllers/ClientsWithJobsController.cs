using AutoWorkshop.Shared.DTOs;
using AutoWorkshopApi.Models;
using AutoWorkshopApi.Repositories;
using AutoWorkshopApi.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

namespace AutoWorkshopApi.Controllers;

[Route("clientswjobs")]
[ApiController]
public class ClientsWithJobsController : ControllerBase
{
    private readonly JobEstimationService _jobEstimationService;
    private readonly IClientRepository _clientRepository;

    public ClientsWithJobsController(IClientRepository clientRepository, JobEstimationService jobEstimationService)
    {
        _jobEstimationService = jobEstimationService;
        _clientRepository = clientRepository;
    }

    //// GET: api/ClientsWithJobs
    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<ClientWithJobsDTO>>> GetClientsWithJobs()
    //{
    //    var clients = await _clientRepository.GetClientWithJobsAsync();
    //    var clientWithJobsDtos = clients.Select(client => new ClientWithJobsDTO
    //    {
    //        ClientId = client.ClientId,
    //        Name = client.Name,
    //        Address = client.Address,
    //        Email = client.Email,
    //        Jobs = client.Jobs.Select(job => new JobDTO
    //        {
    //            JobId = job.JobId,
    //            ClientId = job.ClientId,
    //            LicensePlate = job.LicensePlate,
    //            ManufactureYear = job.ManufactureYear,
    //            Category = job.Category,
    //            Description = job.Description,
    //            Severity = job.Severity,
    //            Status = job.Status,
    //            EstimatedCost = job.EstimatedCost
    //        }).ToList()
    //    }).ToList();

    //    return Ok(clientWithJobsDtos);
    //}

    // GET: api/ClientsWithJobs/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ClientWithJobsDTO>> GetClientWithJobs(int id)
    {
        var client = await _clientRepository.GetClientWithJobsAsync(id);

        if (client == null)
        {
            return NotFound();
        }

        var clientWithJobsDto = new ClientWithJobsDTO
        {
            ClientId = client.ClientId,
            Name = client.Name,
            Address = client.Address,
            Email = client.Email,
            Jobs = client.Jobs.Select(job => new JobDTO
            {
                JobId = job.JobId,
                ClientId = job.ClientId,
                LicensePlate = job.LicensePlate,
                ManufactureYear = job.ManufactureYear,
                Category = job.Category,
                Description = job.Description,
                Severity = job.Severity,
                Status = job.Status.ToString(),
                EstimatedCost = _jobEstimationService.CalculateEstimatedHours(job)
            }).ToList()
        };

        return Ok(clientWithJobsDto);
    }
}
