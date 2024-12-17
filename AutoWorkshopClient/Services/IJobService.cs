using AutoWorkshop.Shared.DTOs;

namespace AutoWorkshop.AutoWorkshopClient.Services;

public interface IJobService
{
    Task<List<JobDTO>> GetAllAsync();
    Task<JobDTO> GetAsync(int jobId);
    Task<double> EstimateCostAsync(JobDTO job);
    Task AddAsync(JobDTO job);
    Task UpdateAsync(JobDTO job);
    Task DeleteAsync(int jobId);
}
