using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SynetecAssessmentApi.Application.Dtos;
using SynetecAssessmentApi.Application.RequestModels;
using SynetecAssessmentApi.Application.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    /// <summary>
    /// Employee Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeDto>))]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"Getting all employees.");

            return Ok(await _employeeService.GetEmployeesAsync());
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Getting employee by id '{id}'.");

            var employee = await _employeeService.GetEmployeeAsync(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        /// <summary>
        /// Add new employee
        /// </summary>
        /// <param name="requestEmployee"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] EmployeeRequestModel requestEmployee)
        {
            _logger.LogInformation($"Create new employee.");

            if (requestEmployee == null)
                return BadRequest();

            var employeeDto = await _employeeService.SaveEmployeeAsync(requestEmployee);

            return Ok(employeeDto);
        }

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestEmployee"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] EmployeeRequestModel requestEmployee)
        {
            _logger.LogInformation($"Updating employee with id '{id}'.");

            if (requestEmployee == null)
                return BadRequest();

            var employee = await _employeeService.GetEmployeeAsync(id);

            if (employee == null)
                return NotFound();

            employee = await _employeeService.UpdateEmployeeAsync(id, requestEmployee);

            return Ok(employee);
        }

        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting employee with id '{id}'.");

            var employee = await _employeeService.GetEmployeeAsync(id);

            if (employee == null)
                return NotFound();

            _employeeService.DeleteEmployeeAsync(id);

            return Ok(employee);
        }
    }
}
