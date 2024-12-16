namespace AutoWorkshop.Shared.DTOs;

using AutoWorkshop.Shared.Enums;
using System.ComponentModel.DataAnnotations;

public class JobDTO
{
    public int JobId { get; set; }

    [Required]
    public int ClientId { get; set; }

    [Required]
    [RegularExpression(@"^[A-Z]{3}-\d{3}$", ErrorMessage = "A rendszám formátuma XXX-YYY")]
    public string LicensePlate { get; set; }

    [Required]
    [Range(1900, int.MaxValue, ErrorMessage = "A gyártási év nem lehet kisebb 1900-nál")]
    public int ManufactureYear { get; set; }

    [Required]
    [EnumDataType(typeof(JobCategory))]
    public string Category { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [Range(1, 10)]
    public int Severity { get; set; }

    [Required]
    [EnumDataType(typeof(JobStatus))]
    public string Status { get; set; }

    public decimal? EstimatedCost { get; set; }
}
