namespace AutoWorkshopApi.Models;

using AutoWorkshop.Shared.Enums;
using System.ComponentModel.DataAnnotations;

public class Job
{
    [Key]
    public int JobId { get; set; }  // Automatikusan generált ID

    [Required]
    public int ClientId { get; set; }  // Ügyfélszám, kapcsolódik az Ügyfélhez

    [Required]
    [RegularExpression(@"^[A-Z]{3}-\d{3}$", ErrorMessage = "A rendszám formátuma XXX-YYY")]
    public string LicensePlate { get; set; }  // Rendszám formátum validációval

    [Required]
    [Range(1900, int.MaxValue, ErrorMessage = "A gyártási év nem lehet kisebb 1900-nál")]
    public int ManufactureYear { get; set; }  // Gyártási év

    [Required]
    [EnumDataType(typeof(JobCategory))]
    public string Category { get; set; }  // Munka kategóriája (pl. Karosszéria)

    [Required]
    public string Description { get; set; }  // Hiba rövid leírása

    [Required]
    [Range(1, 10)]
    public int Severity { get; set; }  // Hiba súlyossága

    [Required]
    [EnumDataType(typeof(JobStatus))]
    public JobStatus Status { get; set; }  // Munka állapota

    public Client Client { get; set; }

    private bool CanTransitionTo(JobStatus newStatus)
    {
        if (newStatus == Status)
        {
            return false; // Can't transition to the same status
        }

        // Check valid transitions
        switch (Status)
        {
            case JobStatus.FelvettMunka:
                return newStatus == JobStatus.ElvegzesAlatt;
            case JobStatus.ElvegzesAlatt:
                return newStatus == JobStatus.Befejezett;
            case JobStatus.Befejezett:
                return false; // No further transitions allowed after "Befejezett"
            default:
                return false;
        }
    }

    // Method to update status, publicly accessible
    public bool UpdateStatus(JobStatus newStatus)
    {
        if (CanTransitionTo(newStatus))
        {
            Status = newStatus;
            return true;
        }
        return false;
    }
}

