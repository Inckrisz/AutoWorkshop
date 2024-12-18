namespace AutoWorkshopApi.Models;

using AutoWorkshop.Shared.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Job
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [NotEmptyOrWhitespace(ErrorMessage = "JobId cannot be empty or contain only whitespace.")]
    public int JobId { get; set; }  

    [Required]
    [NotEmptyOrWhitespace(ErrorMessage = "ClientId cannot be empty or contain only whitespace.")]
    public int ClientId { get; set; }  

    [Required]
    [RegularExpression(@"^[A-Z]{3}-\d{3}$", ErrorMessage = "A rendszám formátuma XXX-YYY")]
    [NotEmptyOrWhitespace(ErrorMessage = "LicensePlate cannot be empty or contain only whitespace.")]
    public string LicensePlate { get; set; } 

    [Required]
    [Range(1900, int.MaxValue, ErrorMessage = "A gyártási év nem lehet kisebb 1900-nál")]
    [NotEmptyOrWhitespace(ErrorMessage = "Category cannot be empty or contain only whitespace.")]
    public int ManufactureYear { get; set; } 

    [Required]
    [EnumDataType(typeof(JobCategory))]
    public string Category { get; set; }  

    [Required]
    [NotEmptyOrWhitespace(ErrorMessage = "Description cannot be empty or contain only whitespace.")]
    public string Description { get; set; }  

    [Required]
    [Range(1, 10)]
    [NotEmptyOrWhitespace(ErrorMessage = "Severity cannot be empty or contain only whitespace.")]
    public int Severity { get; set; }  

    [Required]
    [EnumDataType(typeof(JobStatus))]
    [NotEmptyOrWhitespace(ErrorMessage = "Status cannot be empty or contain only whitespace.")]
    public JobStatus Status { get; set; }  

    public Client Client { get; set; }

    private bool CanTransitionTo(JobStatus newStatus)
    {
        if (newStatus == Status)
        {
            return true; 
        }

        
        switch (Status)
        {
            case JobStatus.FelvettMunka:
                return newStatus == JobStatus.ElvegzesAlatt;
            case JobStatus.ElvegzesAlatt:
                return newStatus == JobStatus.Befejezett;
            case JobStatus.Befejezett:
                return false; 
            default:
                return false;
        }
    }

    
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

