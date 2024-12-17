using System.Net.Http.Json;
using AutoWorkshop.AutoWorkshopClient.Services;
using AutoWorkshop.Shared.DTOs;

namespace AutoWorkshop.AutoWorkshopClient.Services;

public class JobService : IJobService
{
    private readonly HttpClient _httpClient;

    public JobService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<JobDTO>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<JobDTO>>("jobs");
    }

    public async Task<JobDTO> GetAsync(int jobId)
    {   
        return await _httpClient.GetFromJsonAsync<JobDTO>($"jobs/{jobId}");
    }

    public async Task AddAsync(JobDTO job)
    {
        await _httpClient.PostAsJsonAsync("jobs", job);
    }

    public async Task UpdateAsync(JobDTO job)
    {
        await _httpClient.PutAsJsonAsync($"jobs/{job.JobId}", job);
    }

    public async Task DeleteAsync(int jobId)
    {
        await _httpClient.DeleteAsync($"jobs/{jobId}");
    }
}
