using Xunit;
using PL.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using BLL.Interfaces;
using BLL.DTO;
using System.Collections.Generic;
using PL.Models;
using System.Linq;
using System;

namespace TimeTrackingService.Tests
{
    public class ReportControllerTests
    {
        private List<ReportDTO> GetTestReportDTOs()
        {
            var reports = new List<ReportDTO>
            {
                new ReportDTO { Id=1, UserId = 1, Date = new DateTime(2020, 1, 1, 11, 11, 11), Hours = 6},
                new ReportDTO { Id=2, UserId = 1, Date = new DateTime(2020, 1, 1, 11, 11, 11), Hours = 4},
            };
            return reports;
        }

        private List<ReportViewModel> GetTestReportViewModels()
        {
            var reports = new List<ReportViewModel>
            {
                new ReportViewModel { Id=1, UserId = 1, Date = new DateTime(2020, 1, 1, 11, 11, 11), Hours = 6, Note = "note"},
                new ReportViewModel { Id=2, UserId = 1, Date = new DateTime(2020, 1, 1, 11, 11, 11), Hours = 4, Note = "note"},
            };
            return reports;
        }

        [Fact]
        public void GetReportsTest()
        {
            // Arrange
            ReportFilterDTO reportFilterDTO = 
            new ReportFilterDTO
            {
                Month = 1,
                UserId = 1,
                Year = 2020
            };

            var serviceMoq = new Mock<IReportService>();
            serviceMoq.Setup(repo => repo.Get(
                reportFilterDTO))
                .Returns(
                new GetReportsByDateDTO(GetTestReportDTOs()));

            ReportController reportController = new ReportController(serviceMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = reportController.Get(reportFilterDTO);

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ReportViewModel>>(viewResult.Value);
            Assert.Equal(GetTestReportViewModels().Count, model.Count());
        }

        [Fact]
        public void CreateReportReturnsOkAndAddsUser()
        {
            var reportDTO = (new ReportDTO
            {
                UserId = 1,
                Date = new DateTime(2020, 1, 1, 11, 11, 11),
                Hours = 6,
                Note = "note"
            });

            // Arrange
            var serviceMoq = new Mock<IReportService>();
            serviceMoq.Setup(repo => repo.AddReport(reportDTO)).Returns(new ServiceResultDTO(true));

            ReportController reportController = new ReportController(serviceMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = reportController.AddReport(reportDTO);

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            serviceMoq.Verify(r => r.AddReport(reportDTO));
        }

        [Fact]
        public void CreateReportWithModelErrorReturnsValidationErrorMessage()
        {
            var reportDTO = new ReportDTO();
            // Arrange
            var serviceMoq = new Mock<IReportService>();
            ReportController reportController = new ReportController(serviceMoq.Object, AutomapperSingleton.Mapper);
            reportController.ModelState.AddModelError("Note", "Required");

            // Act
            BadRequestObjectResult result = (BadRequestObjectResult)reportController.AddReport(reportDTO);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            SerializableError errorMessages = (SerializableError)result.Value;
            Assert.Equal("Required", ((string[])errorMessages["Note"])[0]);
        }

        [Fact]
        public void UpdateReportReturnsOkAndAddsUser()
        {
            var reportDTO = (new ReportDTO
            {
                Id = 1,
                UserId = 1,
                Date = new DateTime(2020, 1, 1, 11, 11, 11),
                Hours = 6,
                Note = "note"
            });

            // Arrange
            var serviceMoq = new Mock<IReportService>();
            serviceMoq.Setup(repo => repo.UpdateReport(reportDTO)).Returns(new ServiceResultDTO(true));

            ReportController reportController = new ReportController(serviceMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = reportController.UpdateReport(reportDTO);

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            serviceMoq.Verify(r => r.UpdateReport(reportDTO));
        }

        [Fact]
        public void UpdateReportWithModelErrorReturnsValidationErrorMessage()
        {
            var reportDTO = new ReportDTO();
            // Arrange
            var serviceMoq = new Mock<IReportService>();
            ReportController reportController = new ReportController(serviceMoq.Object, AutomapperSingleton.Mapper);
            reportController.ModelState.AddModelError("Name", "Required");

            // Act
            BadRequestObjectResult result = (BadRequestObjectResult)reportController.UpdateReport(reportDTO);

            // Assert
            SerializableError errorMessages = (SerializableError)result.Value;
            Assert.Equal("Required", ((string[])errorMessages["Name"])[0]);
        }

        [Fact]
        public void DeleteUserReturnsOkAndDeleteUser()
        {
            // Arrange
            int reportId = 1;
            var serviceMoq = new Mock<IReportService>();
            serviceMoq.Setup(repo => repo.DeleteReport(reportId)).Returns(new ServiceResultDTO(true));

            ReportController reportController = new ReportController(serviceMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = reportController.DeleteReport(reportId);

            // Assert
            serviceMoq.Verify(r => r.DeleteReport(reportId));
        }
    }
}