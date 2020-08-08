using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.DTO;
using AutoMapper;
using PL.Models;
using Serilog;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

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

        /// <summary>
        //создание пользователя. user/create
        /// </summary>
        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userService.CreateUser(userDTO);
                    return Ok("Пользователь успешно создан");
                }
                else
                {
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                }

            }
            catch (ValidationException ex)
            {
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest("Произошла неизвестная ошибка");
            }
        }

        /// <summary>
        ///обновление данных о пользователе. user/update 
        /// </summary>
        [HttpPut("update")]
        public IActionResult UpdateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userService.UpdateUser(userDTO);

                    return Ok("Данные о пользователе успешно обновлены");
                }
                else
                {
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                }
               
            }
            catch (ValidationException ex)
            {
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest("Произошла неизвестная ошибка");
            }
        }

        /// <summary>
        ///удаление пользователя и связанных с ним отчетов. user/delete
        /// </summary>
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
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest("Произошла неизвестная ошибка");
            }
        }

        /// <summary>
        ///получение списка всех пользователей. user/get
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
                return BadRequest("Произошла неизвестная ошибка");
            }
        }
    }
}
