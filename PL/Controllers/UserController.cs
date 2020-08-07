using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.DTO;
using AutoMapper;
using PL.Models;

namespace PL.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        IUserService userService;

        public UserController(IUserService service)
        {
            userService = service;
        }

        // создание пользователя. user/create
        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                userService.CreateUser(userDTO);

                return Ok("Пользователь успешно создан");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // обновление данных о пользователе. user/update
        [HttpPut("update")]
        public IActionResult UpdateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                userService.UpdateUser(userDTO);

                return Ok("Данные о пользователе успешно обновлены");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        //удаление пользователя и связанных с ним отчетов. user/delete
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int? id)
        {
            try
            {
                userService.DeleteUser(id);
                return Ok("Пользователь успешно удален");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // получение списка всех пользователей. user/get
        [HttpGet("get")]
        public ActionResult GetUsers()
        {
            IEnumerable<UserDTO> userDTOs = userService.GetUsers();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
            var users = mapper.Map<IEnumerable<UserDTO>, List<UserViewModel>>(userDTOs);

            return Ok(users);
        }
    }
}
