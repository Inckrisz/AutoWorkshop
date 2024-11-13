namespace AutoWorkshopApi.Controllers;
using AutoWorkshopApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class JobController : ControllerBase
{
    private readonly AutoWorkshopContext _context;

    public JobController(AutoWorkshopContext context)
    {
        _context = context;
    }

    // GET: api/Clients
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
    {
        return await _context.Jobs.ToListAsync();
    }

    // POST: api/Clients
    [HttpPost]
    public async Task<ActionResult<Job>> PostClient(Job job)
    {
        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetJobs", new { id = job.JobId }, job);
    }

    // PUT, DELETE műveleteket is hozzáadhatunk a CRUD teljesítéséhez
}

