using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Application.Dtos;
using SynetecAssessmentApi.Application.RequestModels;
using SynetecAssessmentApi.Application.Services.Interfaces;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence.Infrastructure;
using SynetecAssessmentApi.Persistence.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Application.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IRepository<Employee> employeeRepository)
            : base(unitOfWork)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            IEnumerable<Employee> employees = await _employeeRepository
                .DbSet
                .Include(e => e.Department)
                .ToListAsync();

            List<EmployeeDto> result = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                result.Add(_mapper.Map<EmployeeDto>(employee));
            }

            return result;
        }

        public async Task<EmployeeDto> GetEmployeeAsync(int id)
        {
            Employee employee = await _employeeRepository.DbSet
                .Include(e => e.Department)
                .FirstOrDefaultAsync(item => item.Id == id);

            if (employee != null)
                return _mapper.Map<EmployeeDto>(employee);

            return null;
        }

        public async Task<EmployeeDto> SaveEmployeeAsync(EmployeeRequestModel employee)
        {
            Employee newEmployee = new Employee(await GetNextEmployeeId(), employee.Fullname, employee.JobTitle, employee.Salary, employee.DepartmentId);

            newEmployee = _employeeRepository.Add(newEmployee);
            await UnitOfWork.SaveChangeAsync();

            newEmployee = await _employeeRepository.DbSet
                .Include(e => e.Department)
                .FirstOrDefaultAsync(item => item.Id == newEmployee.Id);

            return _mapper.Map<EmployeeDto>(newEmployee);
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(int id, EmployeeRequestModel employee)
        {
            var updateEmployee = await _employeeRepository.DbSet
                 .Include(e => e.Department)
                 .FirstOrDefaultAsync(item => item.Id == id);

            updateEmployee.DepartmentId = employee.DepartmentId;
            updateEmployee.Fullname = employee.Fullname;
            updateEmployee.JobTitle = employee.JobTitle;
            updateEmployee.Salary = employee.Salary;

            _employeeRepository.Update(updateEmployee);
            await UnitOfWork.SaveChangeAsync();

            updateEmployee = await _employeeRepository.DbSet
                .Include(e => e.Department)
                .FirstOrDefaultAsync(item => item.Id == id);

            return _mapper.Map<EmployeeDto>(updateEmployee);
        }

        public async void DeleteEmployeeAsync(int id)
        {
            var deleteEmployee = await _employeeRepository.DbSet
                .Include(e => e.Department)
                .FirstOrDefaultAsync(item => item.Id == id);

            if (deleteEmployee != null)
            {
                _employeeRepository.Delete(deleteEmployee);
                await UnitOfWork.SaveChangeAsync();
            }
        }

        private async Task<int> GetNextEmployeeId()
        {
            if (await _employeeRepository.DbSet.CountAsync() == 0)
                return 1;

            return await _employeeRepository.DbSet.MaxAsync(x => x.Id) + 1;
        }
    }
}
