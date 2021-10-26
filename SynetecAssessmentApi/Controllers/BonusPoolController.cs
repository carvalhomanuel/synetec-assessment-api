using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SynetecAssessmentApi.Application.Dtos;
using SynetecAssessmentApi.Application.Services.Interfaces;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BonusPoolController : ControllerBase
    {
        private readonly ILogger<BonusPoolController> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IBonusPoolService _bonusPoolService;

        public BonusPoolController(ILogger<BonusPoolController> logger, IEmployeeService employeeService, IBonusPoolService bonusPoolService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _bonusPoolService = bonusPoolService;
        }

        /// <summary>
        /// Calculate Bonus
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BonusPoolCalculatorResultDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusDto request)
        {
            _logger.LogInformation($"Calculating bonus.");

            if (request == null || request.SelectedEmployeeId == 0) 
            {
                return BadRequest();
            }
            else
            {
                var employee = await _employeeService.GetEmployeeAsync(request.SelectedEmployeeId);

                if (employee == null)
                {
                    return BadRequest();
                }
            }

            return Ok(await _bonusPoolService.CalculateAsync(
                request.TotalBonusPoolAmount,
                request.SelectedEmployeeId));
        }
    }
}
