namespace AutoWorkshop.Shared.DTOs;

using AutoWorkshop.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class JobDTO
{
    [NotEmptyOrWhitespace]
    public int JobId { get; set; }

    [Required]
    [NotEmptyOrWhitespace]
    public int ClientId { get; set; }

    [Required]
    [NotEmptyOrWhitespace]
    [RegularExpression(@"^[A-Z]{3}-\d{3}$", ErrorMessage = "A rendszám formátuma XXX-YYY")]
    public string LicensePlate { get; set; }

    [Required]
    [NotEmptyOrWhitespace]
    [Range(1900, int.MaxValue, ErrorMessage = "A gyártási év nem lehet kisebb 1900-nál")]
    public int ManufactureYear { get; set; }

    [Required]
    [NotEmptyOrWhitespace]
    [EnumDataType(typeof(JobCategory))]
    public string Category { get; set; }

    [Required]
    [NotEmptyOrWhitespace]
    public string Description { get; set; }

    [Required]
    [Range(1, 10)]
    [NotEmptyOrWhitespace]
    public int Severity { get; set; }

    [Required]
    [NotEmptyOrWhitespace]
    [EnumDataType(typeof(JobStatus))]
    public String Status { get; set; }

    public double EstimatedCost { get; set; }
}
