using SynetecAssessmentApi.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Application.Services.Interfaces
{
    public interface IDepartment
    {
        Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();
        Task<DepartmentDto> GetDepartmentAsync(int id);
        Task<DepartmentDto> SaveDepartmentAsync(DepartmentDto department);
        Task<DepartmentDto> UpdateDepartmentAsync(int id, DepartmentDto department);
        void DeleteDepartmentAsync(int id);
    }
}
