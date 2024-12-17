namespace AutoWorkshop.Shared.DTOs;

using System.Collections.Generic;

public class ClientWithJobsDTO
{
    [NotEmptyOrWhitespace]
    public int ClientId { get; set; }
    [NotEmptyOrWhitespace]
    public string Name { get; set; }
    [NotEmptyOrWhitespace]
    public string Address { get; set; }
    [NotEmptyOrWhitespace]
    public string Email { get; set; }

    public List<JobDTO> Jobs { get; set; } = new List<JobDTO>();
}
