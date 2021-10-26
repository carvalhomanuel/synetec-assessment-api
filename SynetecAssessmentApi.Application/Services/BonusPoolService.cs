using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Application.Dtos;
using SynetecAssessmentApi.Application.Services.Interfaces;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence.Infrastructure;
using SynetecAssessmentApi.Persistence.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Application.Services
{
    public class BonusPoolService : BaseService, IBonusPoolService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public BonusPoolService(IUnitOfWork unitOfWork, IRepository<Employee> employeeRepository) : base(unitOfWork)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId)
        {
            //load the details of the selected employee using the Id
            Employee employee = await _employeeRepository.DbSet
                .Include(e => e.Department)
                .FirstOrDefaultAsync(item => item.Id == selectedEmployeeId);

            //get the total salary budget for the company
            int totalSalary = (int)_employeeRepository.DbSet.Sum(item => item.Salary);

            //calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal)employee.Salary / (decimal)totalSalary;
            int bonusAllocation = (int)(bonusPercentage * bonusPoolAmount);

            return new BonusPoolCalculatorResultDto
            {
                Employee = new EmployeeDto
                {
                    Fullname = employee.Fullname,
                    JobTitle = employee.JobTitle,
                    Salary = employee.Salary,
                    Department = new DepartmentDto
                    {
                        Title = employee.Department.Title,
                        Description = employee.Department.Description
                    }
                },

                Amount = bonusAllocation
            };
        }
    }
}
