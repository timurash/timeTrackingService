using Xunit;
using PL.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using BLL.Interfaces;
using BLL.DTO;
using System.Collections.Generic;
using PL.Models;
using System.Linq;

namespace TimeTrackingService.Tests
{
    public class UserControllerTests
    {
        private List<UserDTO> GetTestUserDTOs()
        {
            var users = new List<UserDTO>
            {
                new UserDTO { Id=1, Email = "email1@email.net", Firstname = "Алексей", Surname = "Смирнов", Patronymic = "Валерьевич"},
                new UserDTO { Id=2, Email = "email2@email.net", Firstname = "Александр", Surname = "Галкин", Patronymic = "Федорович"},
                new UserDTO { Id=3, Email = "email3@email.net", Firstname = "Константин", Surname = "Хабенский", Patronymic = "Сергеевич"},
                new UserDTO { Id=4, Email = "email4@email.net", Firstname = "Михаил", Surname = "Прокофьев", Patronymic = "Александрови"},
                new UserDTO { Id=5, Email = "email5@email.net", Firstname = "Роман", Surname = "Стоянов", Patronymic = "Михайлович"}
            };
            return users;
        }

        private List<UserViewModel> GetTestUserViewModels()
        {
            var users = new List<UserViewModel>
            {
                new UserViewModel { Id=1, Email = "email1@email.net", Firstname = "Алексей", Surname = "Смирнов", Patronymic = "Валерьевич"},
                new UserViewModel { Id=2, Email = "email2@email.net", Firstname = "Александр", Surname = "Галкин", Patronymic = "Федорович"},
                new UserViewModel { Id=3, Email = "email3@email.net", Firstname = "Константин", Surname = "Хабенский", Patronymic = "Сергеевич"},
                new UserViewModel { Id=4, Email = "email4@email.net", Firstname = "Михаил", Surname = "Прокофьев", Patronymic = "Александрови"},
                new UserViewModel { Id=5, Email = "email5@email.net", Firstname = "Роман", Surname = "Стоянов", Patronymic = "Михайлович"}
            };
            return users;
        }

        [Fact]
        public void GetUsersTest()
        {
            // Arrange
            var serviceMoq = new Mock<IUserService>();
            serviceMoq.Setup(repo => repo.GetUsers()).Returns(GetTestUserDTOs());

            UserController userController = new UserController(serviceMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = userController.GetUsers();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserViewModel>>(viewResult.Value);
            Assert.Equal(GetTestUserViewModels().Count, model.Count());
        }

        [Fact]
        public void CreateUserReturnsOkAndAddsUser()
        {
            var userDTO = (new UserDTO
            {
                Email = "email@email.net",
                Firstname = "TestName",
                Surname = "TestSurname",
                Patronymic = "TestPatronymic"
            });

            // Arrange
            var serviceMoq = new Mock<IUserService>();
            serviceMoq.Setup(repo => repo.CreateUser(userDTO)).Returns(new ServiceResultDTO(true));

            UserController userController = new UserController(serviceMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = userController.CreateUser(userDTO);

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            serviceMoq.Verify(r => r.CreateUser(userDTO));
        }

        [Fact]
        public void CreateUserWithModelErrorReturnsValidationErrorMessage()
        {
            var userDTO = new UserDTO();
            // Arrange
            var serviceMoq = new Mock<IUserService>();
            UserController userController = new UserController(serviceMoq.Object, AutomapperSingleton.Mapper);
            userController.ModelState.AddModelError("Name", "Required");

            // Act
            BadRequestObjectResult result = (BadRequestObjectResult)userController.CreateUser(userDTO);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            SerializableError errorMessages = (SerializableError)result.Value;
            Assert.Equal("Required", ((string[])errorMessages["Name"])[0]);
        }

        [Fact]
        public void UpdateUserReturnsOkAndAddsUser()
        {
            var userDTO = (new UserDTO
            {
                Id = 1,
                Email = "email@email.net",
                Firstname = "TestName",
                Surname = "TestSurname",
                Patronymic = "TestPatronymic"
            });

            // Arrange
            var serviceMoq = new Mock<IUserService>();
            serviceMoq.Setup(repo => repo.UpdateUser(userDTO)).Returns(new ServiceResultDTO(true));

            UserController userController = new UserController(serviceMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = userController.UpdateUser(userDTO);

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            serviceMoq.Verify(r => r.UpdateUser(userDTO));
        }

        [Fact]
        public void UpdateUserWithModelErrorReturnsValidationErrorMessage()
        {
            var userDTO = new UserDTO();
            // Arrange
            var serviceMoq = new Mock<IUserService>();
            UserController userController = new UserController(serviceMoq.Object, AutomapperSingleton.Mapper);
            userController.ModelState.AddModelError("Name", "Required");

            // Act
            BadRequestObjectResult result = (BadRequestObjectResult)userController.UpdateUser(userDTO);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            SerializableError errorMessages = (SerializableError)result.Value;
            Assert.Equal("Required", ((string[])errorMessages["Name"])[0]);
        }

        [Fact]
        public void DeleteUserReturnsOkAndDeleteUser()
        {
            // Arrange
            int userId = 1;
            var serviceMoq = new Mock<IUserService>();
            serviceMoq.Setup(repo => repo.DeleteUser(userId)).Returns(new ServiceResultDTO(true));

            UserController userController = new UserController(serviceMoq.Object, AutomapperSingleton.Mapper);

            // Act
            var result = userController.DeleteUser(userId);

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            serviceMoq.Verify(r => r.DeleteUser(userId));
        }
    }
}