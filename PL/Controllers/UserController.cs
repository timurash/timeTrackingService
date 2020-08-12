using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.DTO;
using AutoMapper;
using PL.Models;
using Serilog;
using System;
using System.Linq;

namespace PL.Controllers
{
    /// <summary>
    /// Контроллер, предназначенный для работы с пользователями.
    /// Содержит в себе методы создания, удаления, обновления информации о пользователе.
    /// Так же имеет метод получения списка всех пользователей.
    /// </summary>
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        IUserService userService;

        public UserController(IUserService service)
        {
            userService = service;
        }

        /// <summary>
        /// Создание пользователя.
        /// </summary>
        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ServiceResultDTO serviceResult = userService.CreateUser(userDTO);

                    if (serviceResult.IsValid == true)
                    {
                        return Ok("Пользователь успешно создан.");
                    }
                    else
                    {
                        Log.Error(serviceResult.ErrorMessage + userDTO.GetValueString());
                        return BadRequest(serviceResult.ErrorMessage);
                    }
                }
                else
                {
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + userDTO.GetValueString());
                return BadRequest("Произошла неизвестная ошибка.");
            }
        }

        /// <summary>
        /// Обновление данных о пользователе.
        /// </summary>
        [HttpPut("update")]
        public IActionResult UpdateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ServiceResultDTO serviceResult = userService.UpdateUser(userDTO);

                    if (serviceResult.IsValid == true)
                    {
                        return Ok("Данные о пользователе успешно обновлены.");
                    }
                    else
                    {
                        Log.Error(serviceResult.ErrorMessage + userDTO.GetValueString());
                        return BadRequest(serviceResult.ErrorMessage);
                    }
                }
                else
                {
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + userDTO.GetValueString());
                return BadRequest("Произошла неизвестная ошибка.");
            }
        }

        /// <summary>
        /// Удаление пользователя и связанных с ним отчетов.
        /// </summary>
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int? id)
        {
            try
            {
                ServiceResultDTO serviceResult = userService.DeleteUser(id);

                if (serviceResult.IsValid == true)
                {
                    return Ok("Пользователь успешно удален.");
                }
                else
                {
                    Log.Error(serviceResult.ErrorMessage + $"Id = {id}");
                    return BadRequest(serviceResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + $"Id = {id}");
                return BadRequest("Произошла неизвестная ошибка.");
            }
        }

        /// <summary>
        /// Получение списка всех пользователей.
        /// </summary>
        [HttpGet("get")]
        public ActionResult GetUsers()
        {
            try
            {
                IEnumerable<UserDTO> userDTOs = userService.GetUsers();

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
                var users = mapper.Map<IEnumerable<UserDTO>, List<UserViewModel>>(userDTOs);

                return Ok(users);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest("Произошла неизвестная ошибка.");
            }
        }
    }
}