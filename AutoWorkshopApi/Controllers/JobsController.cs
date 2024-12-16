using AutoWorkshop.Shared.DTOs;
using AutoWorkshopApi.Models;
using AutoWorkshopApi.Repositories;
using AutoWorkshopApi.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoWorkshop.Shared.Enums;

namespace AutoWorkshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;
        private readonly JobEstimationService _jobEstimationService;

        public JobController(IJobRepository jobRepository, JobEstimationService jobEstimationService)
        {
            _jobRepository = jobRepository;
            _jobEstimationService = jobEstimationService;
        }

        // GET: api/jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDTO>>> GetJobs()
        {
            var jobs = await _jobRepository.GetAllAsync();

            if (jobs == null)
            {
                return NotFound("No jobs found.");
            }

            // Map entities to DTOs
            var jobDTOs = jobs.Select(job => new JobDTO
            {
                JobId = job.JobId,
                ClientId = job.ClientId,
                LicensePlate = job.LicensePlate,
                ManufactureYear = job.ManufactureYear,
                Category = job.Category.ToString(),
                Description = job.Description,
                Severity = job.Severity,
                Status = job.Status.ToString(),
                EstimatedCost = (decimal?)_jobEstimationService.CalculateEstimatedHours(job)
            }).ToList();

            return Ok(jobDTOs);
        }

        // GET: api/jobs/client/{clientId}
        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<JobDTO>>> GetJobsByClientId(int clientId)
        {
            var jobs = await _jobRepository.GetJobsByClientIdAsync(clientId);

            if (jobs == null || !jobs.Any())
            {
                return NotFound($"No jobs found for client with ID {clientId}.");
            }

            // Map entities to DTOs
            var jobDTOs = jobs.Select(job => new JobDTO
            {
                JobId = job.JobId,
                ClientId = job.ClientId,
                LicensePlate = job.LicensePlate,
                ManufactureYear = job.ManufactureYear,
                Category = job.Category.ToString(),
                Description = job.Description,
                Severity = job.Severity,
                Status = job.Status.ToString(),
                EstimatedCost = (decimal?)_jobEstimationService.CalculateEstimatedHours(job)
            }).ToList();

            return Ok(jobDTOs);
        }

        // POST: api/jobs
        [HttpPost]
        public async Task<ActionResult<JobDTO>> CreateJob(JobDTO jobDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Use Enum.TryParse for safe parsing of the JobStatus and JobCategory enums
            if (!Enum.TryParse<JobCategory>(jobDTO.Category, ignoreCase: true, out var category))
            {
                return BadRequest($"Invalid category value: {jobDTO.Category}");
            }

            if (!Enum.TryParse<JobStatus>(jobDTO.Status, ignoreCase: true, out var status))
            {
                return BadRequest($"Invalid status value: {jobDTO.Status}");
            }

            // Map JobDTO to Job model, excluding the EstimatedCost
            var job = new Job
            {
                ClientId = jobDTO.ClientId,
                LicensePlate = jobDTO.LicensePlate,
                ManufactureYear = jobDTO.ManufactureYear,
                Category = category.ToString(),
                Description = jobDTO.Description,
                Severity = jobDTO.Severity,
                Status = status.ToString()
                // No EstimatedCost here, as it's only needed in the DTO
            };

            // Calculate the EstimatedCost and assign it to the DTO
            jobDTO.JobId = job.JobId;
            jobDTO.EstimatedCost = (decimal?)_jobEstimationService.CalculateEstimatedHours(job);

            await _jobRepository.AddAsync(job);
            await _jobRepository.SaveChangesAsync();

          

            // Return the created job as DTO
            return CreatedAtAction(nameof(GetJobsByClientId), new { clientId = job.ClientId }, jobDTO);
        }


        // PUT: api/jobs/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<JobDTO>> UpdateJob(int id, JobDTO jobDTO)
        {
            if (id != jobDTO.JobId || !ModelState.IsValid)
            {
                return BadRequest("Invalid job data.");
            }

            var job = await _jobRepository.GetByIdAsync(id);

            if (job == null)
            {
                return NotFound($"Job with ID {id} not found.");
            }

            // Update the job details based on the jobDTO
            job.LicensePlate = jobDTO.LicensePlate;
            job.ManufactureYear = jobDTO.ManufactureYear;
            job.Category = jobDTO.Category;

            // Assuming jobDTO.Description and jobDTO.Severity are strings or numbers, no change needed
            job.Description = jobDTO.Description;
            job.Severity = jobDTO.Severity;

            // Ensure job.Status is updated correctly, assuming jobDTO.Status is a string
            job.Status = Enum.TryParse<JobStatus>(jobDTO.Status, out var status) ? status.ToString() : "Unknown";

            // Update the job in the repository
            _jobRepository.Update(job);
            await _jobRepository.SaveChangesAsync();

            // Calculate the EstimatedCost after the update
            jobDTO.EstimatedCost = (decimal?)_jobEstimationService.CalculateEstimatedHours(jobDTO);

            // Return the updated jobDTO with the newly calculated EstimatedCost
            return Ok(jobDTO);  // Return the updated DTO with the estimated cost
        }



        // DELETE: api/jobs/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);

            if (job == null)
            {
                return NotFound($"Job with ID {id} not found.");
            }

            _jobRepository.Delete(job);
            await _jobRepository.SaveChangesAsync();

            return NoContent(); // Success, no content returned
        }
    }
}
