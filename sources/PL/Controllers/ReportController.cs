using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.DTO;
using AutoMapper;
using PL.Models;
using System;
using Serilog;
using Microsoft.AspNetCore.Http;

namespace PL.Controllers
{
    /// <summary>
    /// Контроллер, предназначенный для работы с отчетами пользователей.
    /// Содержит в себе методы создания, удаления, обновления информации об отчетах.
    /// Так же имеет метод получения списка отчетов определенного пользователя за конкретный месяц.
    /// </summary>
    [ApiController]
    [Route("report")]
    public class ReportController : Controller
    {
        readonly IReportService reportService;

        private readonly IMapper _mapper;

        public ReportController(IReportService service, IMapper mapper)
        {
            reportService = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавление отчета.
        /// </summary>
        [HttpPost("add")]
        public IActionResult AddReport([FromBody] ReportDTO reportDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ServiceResultDTO serviceResult = reportService.AddReport(reportDTO);

                    if(serviceResult.IsValid == true)
                    {
                        return Json("Отчет успешно добавлен");
                    }
                    else
                    {
                        Log.Error(serviceResult.ErrorMessage);
                        return BadRequest(serviceResult.ErrorMessage);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }                    
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + reportDTO.GetValueString());
                return BadRequest("Произошла неизвестная ошибка");
            }
        }

        /// <summary>
        /// Обновление отчета.
        /// </summary>
        [HttpPut("update")]
        public IActionResult UpdateReport([FromBody] ReportDTO reportDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ServiceResultDTO serviceResult = reportService.UpdateReport(reportDTO);
                    if (serviceResult.IsValid == true)
                    {
                        return Json("Отчет успешно обновлен");
                    }
                    else
                    {
                        Log.Error(serviceResult.ErrorMessage);
                        return BadRequest(serviceResult.ErrorMessage);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + reportDTO.GetValueString());
                return BadRequest("Произошла неизвестная ошибка");
            }
        }

        /// <summary>
        /// Удаление отчета.
        /// </summary>
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteReport(int? id)
        {
            try
            {
                ServiceResultDTO serviceResult = reportService.DeleteReport(id.Value);

                if (serviceResult.IsValid == true)
                {
                    return Json("Отчет успешно удален");
                }
                else
                {
                    Log.Error(serviceResult.ErrorMessage + $" Id = {id}");
                    return BadRequest(serviceResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + $" Id = {id}");
                return BadRequest("Произошла неизвестная ошибка");
            }
        }

        /// <summary>
        /// Получение списка отчетов определенного пользователя за указанный месяц.
        /// </summary>
        [HttpGet("get")]
        [ProducesResponseType(typeof(List<ReportViewModel>), StatusCodes.Status200OK)]
        public IActionResult Get([FromQuery] ReportFilterDTO reportFilterDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GetReportsByDateDTO getReportsByDateDTO = reportService.Get(reportFilterDTO);

                    if (getReportsByDateDTO.ServiceResultDTO.IsValid == true)
                    {
                        IEnumerable<ReportViewModel> reports = _mapper.Map<IEnumerable<ReportViewModel>>(getReportsByDateDTO.Reports);

                        return Ok(reports);
                    }
                    else
                    {
                        Log.Error(getReportsByDateDTO.ServiceResultDTO.ErrorMessage + reportFilterDTO.GetValueString());
                        return BadRequest(getReportsByDateDTO.ServiceResultDTO.ErrorMessage);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + reportFilterDTO.GetValueString());
                return BadRequest("Произошла неизвестная ошибка");
            }
        }
    }
}