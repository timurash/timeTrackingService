using Xunit;
using Moq;
using BLL.DTO;
using System;
using DAL.Interfaces;
using BLL.Services;
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace TimeTrackingService.Tests
{
    public class ReportServiceTests
    {
        private ReportDTO GetTestReportDTO()
        {
            var reportDTO = (new ReportDTO
            {
                Id = 1,
                UserId = 1,
                Date = new DateTime(2020, 1, 1, 11, 11, 11),
                Hours = 6,
                Note = "note"
            });

            return reportDTO;
        }

        private Report GetTestReport()
        {
            var report = (new Report
            {
                Id = 1,
                UserId = 1,
                Date = new DateTime(2020, 1, 1, 11, 11, 11),
                Hours = 6,
                Note = "note"
            });

            return report;
        }

        private ReportFilterDTO GetTestReportFilterDTO()
        {
            var reportFilterDTO = new ReportFilterDTO
            {
                UserId = 1,
                Month = 1,
                Year = 2020
            };

            return reportFilterDTO;
        }

        private User GetTestUser()
        {
            var testUser = new User
            {
                Id = 1,
                Email = "email@email.net",
                Firstname = "firstname",
                Surname = "surname"
            };

            return testUser;
        }

        private List<ReportDTO> GetTestReportDTOs()
        {
            var reports = new List<ReportDTO>
            {
                new ReportDTO { Id=1, UserId = 1, Date = new DateTime(2020, 1, 1, 11, 11, 11), Hours = 6, Note = "note"},
                new ReportDTO { Id=2, UserId = 1, Date = new DateTime(2020, 1, 1, 11, 11, 11), Hours = 4, Note = "note"},
            };
            return reports;
        }

        private List<Report> GetTestReports()
        {
            var reports = new List<Report>
            {
                new Report { Id=1, UserId = 1, Date = new DateTime(2020, 1, 1, 11, 11, 11), Hours = 6, Note = "note"},
                new Report { Id=2, UserId = 1, Date = new DateTime(2020, 1, 1, 11, 11, 11), Hours = 4, Note = "note"},
            };
            return reports;
        }

        [Fact]
        public void AddReportWithNullUser()
        {
            // Arrange
            var dbMoq = new Mock<IUnitOfWork>();
            dbMoq.Setup(db => db.Users.Get(GetTestReportDTO().UserId.Value)).Returns(value: null);

            ReportService reportService = new ReportService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = reportService.AddReport(GetTestReportDTO());

            // Assert
            Assert.IsType<ServiceResultDTO>(result);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void AddReportAndReturnValidServiceResult()
        {
            // Arrange
            var dbMoq = new Mock<IUnitOfWork>();
            dbMoq.Setup(db => db.Users.Get(GetTestReportDTO().UserId.Value)).Returns(GetTestUser());
            dbMoq.Setup(db => db.Reports.Add(GetTestReport()));

            ReportService reportService = new ReportService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = reportService.AddReport(GetTestReportDTO());

            // Assert
            Assert.IsType<ServiceResultDTO>(result);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void UpdateReportWithNullUser()
        {
            // Arrange
            var dbMoq = new Mock<IUnitOfWork>();
            dbMoq.Setup(db => db.Users.Get(GetTestReportDTO().UserId.Value)).Returns(value: null);

            ReportService reportService = new ReportService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = reportService.UpdateReport(GetTestReportDTO());

            // Assert
            Assert.IsType<ServiceResultDTO>(result);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void UpdateReportWithNullReport()
        {
            // Arrange
            var dbMoq = new Mock<IUnitOfWork>();
            dbMoq.Setup(db => db.Users.Get(GetTestReportDTO().UserId.Value)).Returns(GetTestUser());
            dbMoq.Setup(db => db.Reports.GetById(GetTestReportDTO().Id.Value)).Returns(value: null);

            ReportService reportService = new ReportService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = reportService.UpdateReport(GetTestReportDTO());

            // Assert
            Assert.IsType<ServiceResultDTO>(result);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void UpdateReportAndReturnValidServiceResult()
        {
            // Arrange
            var report = (new Report
            {
                Id = 1,
                UserId = 1,
                Date = new DateTime(2020, 1, 1, 11, 11, 11),
                Hours = 6,
                Note = "note"
            });

            var dbMoq = new Mock<IUnitOfWork>();
            dbMoq.Setup(db => db.Users.Get(GetTestReportDTO().UserId.Value)).Returns(GetTestUser());
            dbMoq.Setup(db => db.Reports.GetById(GetTestReportDTO().Id.Value)).Returns(GetTestReport());
            dbMoq.Setup(db => db.Reports.Update(report));

            ReportService reportService = new ReportService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = reportService.UpdateReport(GetTestReportDTO());

            // Assert
            Assert.IsType<ServiceResultDTO>(result);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void DeleteReportWithNullReportId()
        {
            // Arrange
            int? reportId = null;

            var dbMoq = new Mock<IUnitOfWork>();
            ReportService reportService = new ReportService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = reportService.DeleteReport(reportId);

            // Assert
            Assert.IsType<ServiceResultDTO>(result);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void DeleteReportAndReturnValidServiceResult()
        {
            // Arrange
            var dbMoq = new Mock<IUnitOfWork>();
            dbMoq.Setup(db => db.Reports.GetById(GetTestReportDTO().Id.Value)).Returns(GetTestReport());
            dbMoq.Setup(db => db.Reports.Delete(GetTestReportDTO().Id.Value));

            ReportService reportService = new ReportService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = reportService.DeleteReport(GetTestReportDTO().Id.Value);

            // Assert
            Assert.IsType<ServiceResultDTO>(result);
            Assert.True(result.IsValid);
            dbMoq.Verify(db => db.Reports.Delete(GetTestReportDTO().Id.Value));
        }

        [Fact]
        public void GetReportsWhenUserNotExist()
        {
            // Arrange
            var dbMoq = new Mock<IUnitOfWork>();
            dbMoq.Setup(db => db.Users.Get(GetTestReportFilterDTO().UserId.Value)).Returns(value: null);

            ReportService reportService = new ReportService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = reportService.Get(GetTestReportFilterDTO());

            // Assert
            Assert.IsType<GetReportsByDateDTO>(result);
            Assert.False(result.ServiceResultDTO.IsValid);
        }

        [Fact]
        public void GetReportsAndReturnValidServiceResult()
        {
            // Arrange
            var dbMoq = new Mock<IUnitOfWork>();
            dbMoq.Setup(db => db.Users.Get(GetTestReportFilterDTO().UserId.Value)).Returns(GetTestUser());
            dbMoq.Setup(db => db.Reports.
            GetByUserAndDate(GetTestReportFilterDTO().UserId.Value, GetTestReportFilterDTO().Month.Value, GetTestReportFilterDTO().Year.Value)).
            Returns(GetTestReports());

            ReportService reportService = new ReportService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = reportService.Get(GetTestReportFilterDTO());

            // Assert
            Assert.IsType<GetReportsByDateDTO>(result);
            Assert.True(result.ServiceResultDTO.IsValid);
            dbMoq.Verify(db => db.Reports.
            GetByUserAndDate(GetTestReportFilterDTO().UserId.Value, GetTestReportFilterDTO().Month.Value, GetTestReportFilterDTO().Year.Value));
            Assert.Equal(result.Reports.Count(), GetTestReportDTOs().Count());
        }
    }
}