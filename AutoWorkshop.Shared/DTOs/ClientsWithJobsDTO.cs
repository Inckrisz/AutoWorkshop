namespace AutoWorkshop.Shared.DTOs;

using System.Collections.Generic;

public class ClientWithJobsDTO
{
    public int ClientId { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string Email { get; set; }

    public List<JobDTO> Jobs { get; set; } = new List<JobDTO>();
}
