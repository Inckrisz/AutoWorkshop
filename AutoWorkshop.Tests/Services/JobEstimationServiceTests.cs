using AutoWorkshop.Shared.DTOs;
using Xunit;

public class JobEstimationServiceTests
{
    [Fact]
    public void CalculateEstimatedHours_ShouldReturnCorrectValue()
    {
        // Arrange
        var service = new JobEstimationService();
        var jobDto = new JobDTO
        {
            Category = "Motor",
            ManufactureYear = 10,
            Severity = 5
        };

        // Act
        var result = service.CalculateEstimatedHours(jobDto);

        // Assert
        Assert.Equal(6.0, result); // Replace with the expected value
    }
}
