using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.DTO;
using AutoMapper;
using PL.Models;
using System;
using Serilog;
using System.Linq;
using BLL.Infrastructure;

namespace PL.Controllers
{
    [ApiController]
    [Route("report")]
    public class ReportController : Controller
    {
        IReportService reportService;

        public ReportController(IReportService service)
        {
            reportService = service;
        }

        /// <summary>
        /// добавление отчета. report/add
        /// </summary>
        [HttpPost("add")]
        public IActionResult AddReport([FromBody] ReportDTO reportDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    reportService.AddReport(reportDTO);
                    return Ok("Отчет успешно добавлен");
                }
                else
                {
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                }                    
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest("Произошла неизвестная ошибка");
            }
        }

        /// <summary>
        /// обновление отчета. report/update
        /// </summary>
        [HttpPut("update")]
        public IActionResult UpdateReport([FromBody] ReportDTO reportDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    reportService.UpdateReport(reportDTO);
                    return Ok("Отчет успешно обновлен");
                }
                else
                {
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                }
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest("Произошла неизвестная ошибка");
            }
        }

        /// <summary>
        /// удаление отчета. Пример: report/delete/5
        /// </summary>
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteReport(int? id)
        {
            try
            {
                reportService.DeleteReport(id.Value);
                return Ok("Отчет успешно удален");
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
        /// получение списка отчетов пользователя за указанный месяц
        /// пример запроса: report/get?userid=24&month=1&year=2019
        /// </summary>
        [HttpGet("get")]
        public IActionResult Get([FromQuery] ReportFilterDTO reportFilterDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<ReportDTO> reportDTOs = reportService.Get(reportFilterDTO);
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ReportDTO, ReportViewModel>()).CreateMapper();
                    var reports = mapper.Map<IEnumerable<ReportDTO>, List<ReportViewModel>>(reportDTOs);

                    return Ok(reports);
                }
                else
                {
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                }
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest("Произошла неизвестная ошибка");
            }
        }
    }
}
