using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SynetecAssessmentApi.Application.Dtos;
using SynetecAssessmentApi.Application.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    /// <summary>
    /// Department Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IDepartmentService _departmentService;

        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentService departmentService)
        {
            _logger = logger;
            _departmentService = departmentService;
        }

        /// <summary>
        /// Get all departments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DepartmentDto>))]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"Getting all departments.");

            return Ok(await _departmentService.GetDepartmentsAsync());
        }


        /// <summary>
        /// Get department by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Getting department by id '{id}'.");

            var department = await _departmentService.GetDepartmentAsync(id);

            if (department == null)
                return NotFound();

            return Ok(department);
        }

        /// <summary>
        /// Add new department
        /// </summary>
        /// <param name="requestDepartment"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] DepartmentDto requestDepartment)
        {
            _logger.LogInformation($"Create new department.");

            if (requestDepartment == null)
                return BadRequest();

            var departmentDto = await _departmentService.SaveDepartmentAsync(requestDepartment);

            return Ok(departmentDto);
        }

        /// <summary>
        /// Update department
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestDepartment"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] DepartmentDto requestDepartment)
        {
            _logger.LogInformation($"Updating department with id '{id}'.");

            if (requestDepartment == null)
                return BadRequest();

            var department = await _departmentService.GetDepartmentAsync(id);

            if (department == null)
                return NotFound();

            department = await _departmentService.UpdateDepartmentAsync(id, requestDepartment);

            return Ok(department);
        }


        /// <summary>
        /// Delete department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting department with id '{id}'.");

            var department = await _departmentService.GetDepartmentAsync(id);

            if (department == null)
                return NotFound();

            _departmentService.DeleteDepartmentAsync(id);

            return Ok(department);
        }
    }
}
