using AutoWorkshop.Shared.DTOs;
using AutoWorkshopApi.Models;
using AutoWorkshopApi.Repositories;
using AutoWorkshopApi.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

namespace AutoWorkshopApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientRepository _clientRepository;

    public ClientsController(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    // GET: api/Client
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClients()
    {
        var clients = await _clientRepository.GetAllAsync();
        var clientDtos = clients.Select(client => new ClientDTO
        {
            
            ClientId = client.ClientId,
            Name = client.Name,
            Address = client.Address,
            Email = client.Email
        }).ToList();

        return Ok(clientDtos);
    }

    // GET: api/Client/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDTO>> GetClient(int id)
    {
        var client = await _clientRepository.GetClientWithJobsAsync(id);

        if (client == null)
        {
            return NotFound();
        }

        var clientDto = new ClientDTO
        {
            ClientId = client.ClientId,
            Name = client.Name,
            Address = client.Address,
            Email = client.Email
        };

        return Ok(clientDto);
    }

    // POST: api/Client
    [HttpPost]
    public async Task<ActionResult<ClientDTO>> CreateClient(ClientDTO clientDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var client = new Client
        {
            Name = clientDto.Name,
            Address = clientDto.Address,
            Email = clientDto.Email
        };







        await _clientRepository.AddAsync(client);
        await _clientRepository.SaveChangesAsync();

        clientDto.ClientId = client.ClientId;

        return CreatedAtAction(nameof(GetClient), new { id = client.ClientId }, clientDto);
    }

    // PUT: api/Client/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(int id, ClientDTO clientDto)
    {
        if (id != clientDto.ClientId)
        {
            return BadRequest("Client ID mismatch.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingClient = await _clientRepository.GetByIdAsync(id);
        if (existingClient == null)
        {
            return NotFound();
        }

        existingClient.Name = clientDto.Name;
        existingClient.Address = clientDto.Address;
        existingClient.Email = clientDto.Email;

        _clientRepository.Update(existingClient);
        await _clientRepository.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Client/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client == null)
        {
            return NotFound();
        }

        _clientRepository.Delete(client);
        await _clientRepository.SaveChangesAsync();

        return NoContent();
    }
}
