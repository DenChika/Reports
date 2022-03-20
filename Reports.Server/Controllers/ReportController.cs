using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Conditions;
using Reports.DAL.Entities;
using Reports.Server.Services;
using Task = Reports.DAL.Entities.Task;

namespace Reports.Server.Controllers
{
    [Route("/reports")]
    public class ReportController
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<Report> Create([FromQuery] Guid employeeId, [FromQuery] int days)
        {
            return await _service.Create(employeeId, days);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<Report>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpGet]
        [Route("GetWeekTasks")]
        public List<Task> GetWeekTasks([FromQuery] DateTime reportDate)
        {
            return _service.GetWeekTasks(reportDate);
        }

        [HttpGet]
        [Route("GetBySubordinates")]
        public List<Report> GetBySubordinates([FromQuery] Guid employeeId, [FromQuery] DateTime reportDate, [FromQuery] int days)
        {
            return _service.GetBySubordinates(employeeId, reportDate, days);
        }

        [HttpPatch]
        [Route("AddTask")]
        public void AddTask([FromQuery] Guid taskId, [FromQuery] Guid reportId)
        {
            _service.AddTask(taskId, reportId);
        }

        [HttpPut]
        [Route("UpdateRedactorAccess")]
        public void UpdateRedactorAccess([FromQuery] bool redactorAccess, [FromQuery] Guid reportId)
        {
            _service.UpdateRedactorAccess(redactorAccess, reportId);
        }
    }
}