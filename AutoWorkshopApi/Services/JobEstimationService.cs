using AutoWorkshop.Shared.DTOs;
using AutoWorkshopApi.Models;

public class JobEstimationService
{
    public double CalculateEstimatedHours(Job job)
    {
        return CalculateEstimatedHoursInternal(job.Category, job.ManufactureYear, job.Severity);
    }

    public double CalculateEstimatedHours(JobDTO jobDto)
    {
        return CalculateEstimatedHoursInternal(jobDto.Category, jobDto.ManufactureYear, jobDto.Severity);
    }

    private double CalculateEstimatedHoursInternal(string category, int manufactureYear, int severity)
    {
        int year = DateTime.Now.Year - manufactureYear;
        

        double categoryMultiplier = category switch
        {
            "Karosszéria" => 3,
            "Motor" => 8,
            "Futómű" => 6,
            "Fékberendezés" => 4,
            _ => 0
        };

        double ageMultiplier = year switch
        {
            <= 5 => 0.5,
            <= 10 => 1,
            <= 20 => 1.5,
            _ => 2
        };

        double severityMultiplier = severity switch
        {
            <= 2 => 0.2,
            <= 4 => 0.4,
            <= 7 => 0.6,
            <= 9 => 0.8,
            10 => 1,
            _ => 0
        };

        return categoryMultiplier * ageMultiplier * severityMultiplier;
    }
}
