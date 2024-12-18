﻿using AutoWorkshop.Shared.DTOs;
using AutoWorkshopApi.Models;

public class JobEstimationService
{
    public virtual double CalculateEstimatedHours(Job job)
    {
        return CalculateEstimatedHoursInternal(job.Category, job.ManufactureYear, job.Severity);
    }

    public virtual double CalculateEstimatedHours(JobDTO jobDto)
    {
        return CalculateEstimatedHoursInternal(jobDto.Category, jobDto.ManufactureYear, jobDto.Severity);
    }

    private double CalculateEstimatedHoursInternal(string category, int manufactureYear, int severity)
    {
        int year = DateTime.Now.Year - manufactureYear;

        if (year < 0)
        {
            throw new ArgumentException("Manufacture year cannot be in the future.");
        }
        

        double categoryMultiplier = category switch
        {
            "Karosszeria" => 3,
            "Motor" => 8,
            "Futomu" => 6,
            "Fekberendezes" => 4,
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

        return Math.Round(categoryMultiplier * ageMultiplier * severityMultiplier,2);
    }
}
