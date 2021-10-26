using SynetecAssessmentApi.Application.Dtos;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Application.Services.Interfaces
{
    public interface ICalculation
    {
        Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId);
    }
}
