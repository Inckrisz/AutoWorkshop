using System.ComponentModel.DataAnnotations;
using AutoWorkshopApi.Models;
using AutoWorkshop.Shared.Enums;

namespace AutoWorkshopApi.Tests;

public class JobUnitTests
{
    [Fact]
    public void ValidJob_JobValidation_IsValid()
    {
    
        var job = new Job
        {
            ClientId = 1,
            LicensePlate = "ABC-123",
            ManufactureYear = 2020,
            Category = JobCategory.Karosszeria.ToString(),
            Description = "Fender damage",
            Severity = 5,
            Status = JobStatus.FelvettMunka
        };

      
        var results = new List<ValidationResult>();
        var context = new ValidationContext(job, null, null);
        var result = Validator.TryValidateObject(job, context, results, true);

        Assert.True(result);
        Assert.Empty(results);
    }

    [Fact]
    public void InvalidLicensePlate_JobValidation_Fails()
    {
       
        var job = new Job
        {
            ClientId = 1,
            LicensePlate = "INVALID", 
            ManufactureYear = 2020,
            Category = JobCategory.Karosszeria.ToString(),
            Description = "Fender damage",
            Severity = 5,
            Status = JobStatus.FelvettMunka,
        };

      
        var results = new List<ValidationResult>();
        var context = new ValidationContext(job, null, null);
        var result = Validator.TryValidateObject(job, context, results, true);

     
        Assert.False(result);
        Assert.Contains(results, v => v.MemberNames.Contains("LicensePlate") && v.ErrorMessage.Contains("A rendszám formátuma XXX-YYY"));
    }

    [Fact]
    public void ManufactureYearBefore1900_JobValidation_Fails()
    {
      
        var job = new Job
        {
            ClientId = 1,
            LicensePlate = "ABC-123",
            ManufactureYear = 1899, 
            Category = JobCategory.Karosszeria.ToString(),
            Description = "Fender damage",
            Severity = 5,
            Status = JobStatus.FelvettMunka,
        };

       
        var results = new List<ValidationResult>();
        var context = new ValidationContext(job, null, null);
        var result = Validator.TryValidateObject(job, context, results, true);

      
        Assert.False(result);
        Assert.Contains(results, v => v.MemberNames.Contains("ManufactureYear") && v.ErrorMessage.Contains("A gyártási év nem lehet kisebb 1900-nál"));
    }

    [Fact]
    public void InvalidSeverity_JobValidation_Fails()
    {
        
        var job = new Job
        {
            ClientId = 1,
            LicensePlate = "ABC-123",
            ManufactureYear = 2020,
            Category = JobCategory.Karosszeria.ToString(),
            Description = "Fender damage",
            Severity = 11, 
            Status = JobStatus.FelvettMunka,
        };

       
        var results = new List<ValidationResult>();
        var context = new ValidationContext(job, null, null);
        var result = Validator.TryValidateObject(job, context, results, true);

        
        Assert.False(result); 
        Assert.Contains(results, v => v.MemberNames.Contains("Severity") && v.ErrorMessage.Contains("The field Severity must be between 1 and 10."));
    }

    [Fact]
    public void StatusTransition_ValidTransition_IsSuccessful()
    {
        
        var job = new Job
        {
            ClientId = 1,
            LicensePlate = "ABC-123",
            ManufactureYear = 2020,
            Category = JobCategory.Karosszeria.ToString(),
            Description = "Fender damage",
            Severity = 5,
            Status = JobStatus.FelvettMunka,
        };

      
        var result = job.UpdateStatus(JobStatus.ElvegzesAlatt);

        
        Assert.True(result); 
        Assert.Equal(JobStatus.ElvegzesAlatt, job.Status);
    }

    [Fact]
    public void StatusTransition_InvalidTransition_Fails()
    {
        
        var job = new Job
        {
            ClientId = 1,
            LicensePlate = "ABC-123",
            ManufactureYear = 2020,
            Category = JobCategory.Karosszeria.ToString(),
            Description = "Fender damage",
            Severity = 5,
            Status = JobStatus.Befejezett, 
        };

       
        var result = job.UpdateStatus(JobStatus.ElvegzesAlatt);

      
        Assert.False(result); 
        Assert.Equal(JobStatus.Befejezett, job.Status); 
    }
}
