using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Singulare.DTOs;
using Singulare.Messaging;
using Singulare.Services.ProcessReportService;
using System;

namespace Singulare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessReportController : ControllerBase
    {
        private readonly IMessageBusService _messageBusService;

        private readonly IProcessReportService _processReportService;

        public ProcessReportController(IMessageBusService messageBusService, IProcessReportService processReportService)
        {
            _messageBusService = messageBusService;
            _processReportService = processReportService;
        }

        [HttpPost()]
        public ActionResult<List<ProcessReports>> Process(CreateProcessReportDto processReport)
        {
            try
            {
                var processReportCreated = _processReportService.CreateProcessReport(processReport);
                _messageBusService.publish(processReportCreated, "process-report");
                return Ok(processReportCreated);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProcessReports>>> GetProcessByid(int id)
        {
            try
            {
                var processReportFound = await _processReportService.GetProcessReportById(id);
                
                return Ok(processReportFound);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
