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
    public class UserServiceTests
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

        private User GetTestUser2()
        {
            var testUser = new User
            {
                Id = 2,
                Email = "email@email.net",
                Firstname = "firstname",
                Surname = "surname"
            };

            return testUser;
        }

        private UserDTO GetTestUserDTO()
        {
            var testUser = new UserDTO
            {
                Id = 1,
                Email = "email@email.net",
                Firstname = "firstname",
                Surname = "surname"
            };

            return testUser;
        }

        private List<User> GetTestUsers()
        {
            var users = new List<User>
            {
                new User { Id=1, Email = "email1@email.net", Firstname = "Алексей", Surname = "Смирнов", Patronymic = "Валерьевич"},
                new User { Id=2, Email = "email2@email.net", Firstname = "Александр", Surname = "Галкин", Patronymic = "Федорович"},
                new User { Id=3, Email = "email3@email.net", Firstname = "Константин", Surname = "Хабенский", Patronymic = "Сергеевич"},
                new User { Id=4, Email = "email4@email.net", Firstname = "Михаил", Surname = "Прокофьев", Patronymic = "Александрови"},
                new User { Id=5, Email = "email5@email.net", Firstname = "Роман", Surname = "Стоянов", Patronymic = "Михайлович"}
            };
            return users;
        }

        [Fact]
        public void CreateUserWithNotUniqueEmail()
        {
            // Arrange
            var dbMoq = new Mock<IUnitOfWork>();
            dbMoq.Setup(db => db.Users.Find(GetTestUserDTO().Email)).Returns(GetTestUser2());

            UserService userService = new UserService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = userService.CreateUser(GetTestUserDTO());

            // Assert
            Assert.IsType<ServiceResultDTO>(result);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void CreateUserAndReturnValidServiceResult()
        {
            // Arrange
            var dbMoq = new Mock<IUnitOfWork>();
            dbMoq.Setup(db => db.Users.Get(GetTestUserDTO().Id)).Returns(GetTestUser());
            dbMoq.Setup(db => db.Users.Create(GetTestUser()));

            UserService userService = new UserService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = userService.CreateUser(GetTestUserDTO());

            // Assert
            Assert.IsType<ServiceResultDTO>(result);
            Assert.True(result.IsValid);
        }

        
        [Fact]
        public void UpdateUserWithNullUser()
        {
            // Arrange
            var dbMoq = new Mock<IUnitOfWork>();
            dbMoq.Setup(db => db.Users.Get(GetTestUserDTO().Id)).Returns(value: null);

            UserService userService = new UserService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = userService.UpdateUser(GetTestUserDTO());

            // Assert
            Assert.IsType<ServiceResultDTO>(result);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void UpdateUserWithNotUniqueEmail()
        {
            // Arrange
            var dbMoq = new Mock<IUnitOfWork>();
            dbMoq.Setup(db => db.Users.Get(GetTestUserDTO().Id)).Returns(GetTestUser());
            dbMoq.Setup(db => db.Users.Find(GetTestUserDTO().Email)).Returns(GetTestUser2());

            UserService userService = new UserService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = userService.UpdateUser(GetTestUserDTO());

            // Assert
            Assert.IsType<ServiceResultDTO>(result);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void UpdateUserAndReturnValidServiceResult()
        {
            // Arrange
            var dbMoq = new Mock<IUnitOfWork>();
            dbMoq.Setup(db => db.Users.Get(GetTestReportDTO().UserId.Value)).Returns(GetTestUser());
            dbMoq.Setup(db => db.Users.Find(GetTestUserDTO().Email)).Returns(GetTestUser());
            dbMoq.Setup(db => db.Users.Update(GetTestUser()));

            UserService userService = new UserService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = userService.UpdateUser(GetTestUserDTO());

            // Assert
            Assert.IsType<ServiceResultDTO>(result);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void DeleteUserIfUserNotExist()
        {
            // Arrange
            int userId = 1;

            var dbMoq = new Mock<IUnitOfWork>();
            dbMoq.Setup(db => db.Users.Get(userId)).Returns(value: null);
            UserService userService = new UserService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = userService.DeleteUser(userId);

            // Assert
            Assert.IsType<ServiceResultDTO>(result);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void DeleteUserAndReturnValidServiceResult()
        {
            // Arrange
            int userId = 1;

            var dbMoq = new Mock<IUnitOfWork>();
            dbMoq.Setup(db => db.Users.Get(userId)).Returns(GetTestUser());
            dbMoq.Setup(db => db.Reports.DeleteByUser(userId));
            dbMoq.Setup(db => db.Users.Delete(userId));

            UserService userService = new UserService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = userService.DeleteUser(userId);

            // Assert
            Assert.IsType<ServiceResultDTO>(result);
            Assert.True(result.IsValid);
            dbMoq.Verify(r => r.Users.Delete(userId));
        }

        [Fact]
        public void GetReportsAndReturnUsers()
        {
            // Arrange
            var dbMoq = new Mock<IUnitOfWork>();
            dbMoq.Setup(db => db.Users.Get(GetTestReportFilterDTO().UserId.Value)).Returns(GetTestUser());
            dbMoq.Setup(db => db.Users.GetAll()).Returns(GetTestUsers());

            UserService userService = new UserService(dbMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = userService.GetUsers();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<UserDTO>>(result);
            Assert.Equal(result.Count(), GetTestUsers().Count());
        }
    }
}