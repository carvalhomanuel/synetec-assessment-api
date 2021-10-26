using SynetecAssessmentApi.Application.Dtos;
using SynetecAssessmentApi.Application.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Application.Services.Interfaces
{
    public interface IEmployee
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        Task<EmployeeDto> GetEmployeeAsync(int id);
        Task<EmployeeDto> SaveEmployeeAsync(EmployeeRequestModel employee);
        Task<EmployeeDto> UpdateEmployeeAsync(int id, EmployeeRequestModel employee);
        void DeleteEmployeeAsync(int id);
    }
}
