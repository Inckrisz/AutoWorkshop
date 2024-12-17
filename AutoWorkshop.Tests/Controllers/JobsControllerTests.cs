using Moq;
using Xunit;
using AutoWorkshopApi.Controllers;
using AutoWorkshopApi.Models;
using AutoWorkshopApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoWorkshop.Shared.DTOs;
using AutoWorkshop.Shared.Enums;
using AutoWorkshopApi.Repositories.Base;

namespace AutoWorkshopApi.Tests
{
    public class JobsControllerTests
    {
        private readonly Mock<IJobRepository> _jobRepositoryMock;
        private readonly Mock<JobEstimationService> _jobEstimationServiceMock;
        private readonly JobsController _controller;

        public JobsControllerTests()
        {
            _jobRepositoryMock = new Mock<IJobRepository>();
            _jobEstimationServiceMock = new Mock<JobEstimationService>();
            _controller = new JobsController(_jobRepositoryMock.Object, _jobEstimationServiceMock.Object);
        }

        
        [Fact]
        public async Task GetJobs_ReturnsOkResult_WhenNoJobsExist()
        {
            // Arrange
            _jobRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Job>());

            // Act
            var result = await _controller.GetJobs();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var jobDTOs = Assert.IsType<List<JobDTO>>(okResult.Value);
            Assert.Empty(jobDTOs); // Verify the list is empty
        }


        


        [Fact]
        public async Task CreateJob_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var jobDTO = new JobDTO(); // Invalid DTO (missing required fields)

            _controller.ModelState.AddModelError("ClientId", "Required");

            // Act
            var result = await _controller.CreateJob(jobDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var badRequestValue = badRequestResult.Value as SerializableError;
            Assert.NotNull(badRequestValue);
            Assert.True(badRequestValue.ContainsKey("ClientId"));
        }


        [Fact]
        public async Task DeleteJob_ReturnsNoContent_WhenJobIsDeleted()
        {
            // Arrange
            var job = new Job { JobId = 1, ClientId = 1, LicensePlate = "XYZ123" };

            _jobRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(job);
            _jobRepositoryMock.Setup(repo => repo.Delete(job)).Verifiable();
            _jobRepositoryMock.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteJob(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _jobRepositoryMock.Verify();
        }

        [Fact]
        public async Task DeleteJob_ReturnsNotFound_WhenJobDoesNotExist()
        {
            // Arrange
            _jobRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Job)null);

            // Act
            var result = await _controller.DeleteJob(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateJob_ReturnsBadRequest_WhenStatusTransitionIsInvalid()
        {
            // Arrange
            var jobDTO = new JobDTO { JobId = 1, Status = "Befejezett" }; // Invalid status transition
            var existingJob = new Job { JobId = 1, Status = JobStatus.FelvettMunka };

            _jobRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingJob);

            // Act
            var result = await _controller.UpdateJob(1, jobDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Contains("Invalid status transition", badRequestResult.Value.ToString());
        }
    }
}
