using System;
using AutoWorkshop.Shared.DTOs;
using AutoWorkshopApi.Models;
using Xunit;

public class JobEstimationServiceTests
{
    private readonly JobEstimationService _service;

    public JobEstimationServiceTests()
    {
        _service = new JobEstimationService();
    }

    [Theory]
    [InlineData("Karosszéria", 2018, 5, 1.8)] // categoryMultiplier = 3, ageMultiplier = 0.5, severityMultiplier = 0.6
    [InlineData("Motor", 2010, 9, 9.6)]       // categoryMultiplier = 8, ageMultiplier = 1.5, severityMultiplier = 0.8
    [InlineData("Futómű", 2000, 10, 12.0)]    // categoryMultiplier = 6, ageMultiplier = 2, severityMultiplier = 1
    [InlineData("Fékberendezés", 2022, 2, 0.4)] // categoryMultiplier = 4, ageMultiplier = 0.5, severityMultiplier = 0.2
    [InlineData("Motor", 2024, 4, 1.6)]
    [InlineData("Motor", 2024, 11, 0)]
    [InlineData("Invalid", 1995, 10, 0)]      // categoryMultiplier = 0
    public void CalculateEstimatedHours_WithJob_ReturnsCorrectValue(string category, int manufactureYear, int severity, double expected)
    {
        // Arrange
        var job = new Job
        {
            Category = category,
            ManufactureYear = manufactureYear,
            Severity = severity
        };

        // Act
        var result = _service.CalculateEstimatedHours(job);

        // Assert
        Assert.Equal(expected, result, 2); // Tolerance for floating-point comparison
    }

    

    [Fact]
    public void CalculateEstimatedHours_WithInvalidCategory_ReturnsZero()
    {
        // Arrange
        var job = new Job
        {
            Category = "Invalid",
            ManufactureYear = 2010,
            Severity = 5
        };

        // Act
        var result = _service.CalculateEstimatedHours(job);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateEstimatedHours_WithFutureYear_ThrowsException()
    {
        // Arrange
        var job = new Job
        {
            Category = "Karosszéria",
            ManufactureYear = DateTime.Now.Year + 1,
            Severity = 5
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.CalculateEstimatedHours(job));
    }
}
