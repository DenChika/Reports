using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<Employee> Create([FromQuery] string name, [FromQuery] Guid bossId)
        {
            return await _service.Create(name, bossId);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<Employee>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpGet]
        [Route("FindByName")]
        public async Task<IActionResult> Find([FromQuery] string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Employee result = await _service.FindByName(name);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("FindById")]
        public async Task<IActionResult> Find([FromQuery] Guid id)
        {
            if (id != Guid.Empty)
            {
                Employee result = await _service.FindById(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpPut]
        [Route("UpdateName")]
        public async Task<Employee> UpdateName([FromQuery] Guid entityId, [FromQuery] string name)
        {
            return await _service.UpdateName(entityId, name);
        }

        [HttpPut]
        [Route("UpdateBoss")]
        public async Task<Employee> UpdateBoss([FromQuery] Guid entityId, [FromQuery] Guid bossId)
        {
            return await _service.UpdateBoss(entityId, bossId);
        }

        [HttpDelete]
        [Route("Delete")]
        public void Delete([FromQuery] Guid id)
        {
            _service.Delete(id);
        }
    }
} 