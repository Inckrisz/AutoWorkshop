using AutoWorkshop.Shared.DTOs;

namespace AutoWorkshop.AutoWorkshopClient.Services;

public interface IJobService
{
    Task<List<JobDTO>> GetAllAsync();
    Task<JobDTO> GetAsync(int jobId);
    Task AddAsync(JobDTO job);
    Task UpdateAsync(JobDTO job);
    Task DeleteAsync(int jobId);
}
