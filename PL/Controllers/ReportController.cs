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
    [Route("report")]
    public class ReportController : Controller
    {
        IReportService reportService;

        public ReportController(IReportService service)
        {
            reportService = service;
        }

        // добавление отчета. report/add
        [HttpPost("add")]
        public IActionResult AddReport([FromBody] ReportDTO reportDTO)
        {
            try
            {
                reportService.AddReport(reportDTO);
                return Ok("Отчет успешно добавлен");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // обновление отчета. report/update
        [HttpPut("update")]
        public IActionResult UpdateReport([FromBody] ReportDTO reportDTO)
        {
            try
            {
                reportService.UpdateReport(reportDTO);
                return Ok("Отчет успешно обновлен");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // удаление отчета. Пример: report/delete/5
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
                return Content(ex.Message);
            }
        }

        // получение списка отчетов пользователя за указанный месяц
        // пример запроса: report/get?userid=24&month=1&year=2019
        [HttpGet("get")]
        public IActionResult Get([FromQuery] ReportFilterDTO reportFilterDTO)
        {
            try
            {
                IEnumerable<ReportDTO> reportDTOs = reportService.Get(reportFilterDTO);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ReportDTO, ReportViewModel>()).CreateMapper();
                var reports = mapper.Map<IEnumerable<ReportDTO>, List<ReportViewModel>>(reportDTOs);

                return Ok(reports);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
