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
    [Route("/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<Task> Create([FromQuery] Guid executorId)
        {
            return await _service.Create(executorId);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<Task>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpPut]
        [Route("ChangeCondition")]
        public void ChangeCondition([FromQuery] Guid taskId, [FromQuery] Guid employeeId, [FromQuery] Condition condition)
        {
            _service.ChangeCondition(taskId, employeeId, condition);
        }

        [HttpPatch]
        [Route("AddComment")]
        public void AddComment([FromQuery] Guid taskId, [FromQuery] Guid employeeId, [FromQuery] string comment)
        {
            _service.AddComment(taskId, employeeId, comment);
        }

        [HttpGet]
        [Route("FindById")]
        public async Task<IActionResult> FindById([FromQuery] Guid taskId)
        {
            if (taskId != Guid.Empty)
            {
                Task result = await _service.FindById(taskId);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("FindByCreateTime")]
        public IActionResult FindByCreateTime([FromQuery] DateTime createTime)
        {
            if (createTime != DateTime.Parse(string.Empty))
            {
                Task result = _service.FindByCreateDate(createTime);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("FindByUpdateTime")]
        public IActionResult FindByUpdateTime([FromQuery] DateTime lastUpdateTime)
        {
            if (lastUpdateTime != DateTime.Parse(string.Empty))
            {
                Task result = _service.FindByLastUpdateDate(lastUpdateTime);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("FindByExecutor")]
        public IActionResult FindByExecutor([FromQuery] Guid executorId)
        {
            if (executorId != Guid.Empty)
            {
                List<Task> result = _service.FindByExecutor(executorId);
                if (result.Count != 0)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("FindUpdateByEmployee")]
        public IActionResult FindUpdateByEmployee([FromQuery] Guid employeeId)
        {
            if (employeeId != Guid.Empty)
            {
                List<Task> result = _service.FindUpdateByEmployee(employeeId);
                if (result.Count != 0)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("FindUpdateBySubordinates")]
        public IActionResult FindBySubordinates([FromQuery] Guid employeeId)
        {
            if (employeeId != Guid.Empty)
            {
                List<Task> result = _service.FindBySubordinates(employeeId);
                if (result.Count != 0)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }
    }
} 