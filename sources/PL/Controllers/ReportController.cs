using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.DTO;
using AutoMapper;
using PL.Models;
using System;
using Serilog;
using System.Linq;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddReport([FromBody] ReportDTO reportDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ServiceResultDTO serviceResult = reportService.AddReport(reportDTO);

                    if(serviceResult.IsValid == true)
                    {
                        return Ok("Отчет успешно добавлен.");
                    }
                    else
                    {
                        Log.Error(serviceResult.ErrorMessage);
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
                Log.Error(ex.Message + reportDTO.GetValueString());
                return BadRequest("Произошла неизвестная ошибка.");
            }
        }

        /// <summary>
        /// Обновление отчета.
        /// </summary>
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateReport([FromBody] ReportDTO reportDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ServiceResultDTO serviceResult = reportService.UpdateReport(reportDTO);
                    if (serviceResult.IsValid == true)
                    {
                        return Ok("Отчет успешно обновлен.");
                    }
                    else
                    {
                        Log.Error(serviceResult.ErrorMessage);
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
                Log.Error(ex.Message + reportDTO.GetValueString());
                return BadRequest("Произошла неизвестная ошибка.");
            }
        }

        /// <summary>
        /// Удаление отчета. Пример: report/delete/5.
        /// </summary>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteReport(int? id)
        {
            try
            {
                ServiceResultDTO serviceResult = reportService.DeleteReport(id.Value);

                if (serviceResult.IsValid == true)
                {
                    return Ok("Отчет успешно удален.");
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
                return BadRequest("Произошла неизвестная ошибка.");
            }
        }

        /// <summary>
        /// Получение списка отчетов определенного пользователя за указанный месяц.
        /// Пример запроса: report/get?userid=24&month=1&year=2019
        /// </summary>
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + reportFilterDTO.GetValueString());
                return BadRequest("Произошла неизвестная ошибка.");
            }
        }
    }
}