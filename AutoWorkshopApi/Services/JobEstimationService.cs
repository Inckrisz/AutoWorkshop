
using AutoWorkshopApi.Models;
public class JobEstimationService
{
    public double CalculateEstimatedHours(Job job)
    {
        double categoryMultiplier = job.Category switch
        {
            "Karosszéria" => 3,
            "Motor" => 8,
            "Futómű" => 6,
            "Fékberendezés" => 4,
            _ => 0
        };

        double ageMultiplier = job.ManufactureYear switch
        {
            <= 5 => 0.5,
            <= 10 => 1,
            <= 20 => 1.5,
            _ => 2
        };

        double severityMultiplier = job.Severity switch
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
