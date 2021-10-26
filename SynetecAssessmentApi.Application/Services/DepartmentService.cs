using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Application.Dtos;
using SynetecAssessmentApi.Application.Services.Interfaces;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence.Infrastructure;
using SynetecAssessmentApi.Persistence.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Application.Services
{
    public class DepartmentService : BaseService, IDepartmentService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Department> _departmentRepository;

        public DepartmentService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IRepository<Department> departmentRepository)
            : base(unitOfWork)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
        {
            IEnumerable<Department> departments = await _departmentRepository
                .DbSet
                .ToListAsync();

            List<DepartmentDto> result = new List<DepartmentDto>();

            foreach (var department in departments)
            {
                result.Add(_mapper.Map<DepartmentDto>(department));
            }

            return result;
        }

        public async Task<DepartmentDto> GetDepartmentAsync(int id)
        {
            Department department = await _departmentRepository.GetAsync(id);

            if (department != null)
                return _mapper.Map<DepartmentDto>(department);

            return null;
        }

        public async Task<DepartmentDto> SaveDepartmentAsync(DepartmentDto department)
        {
            Department newDepartment = new Department(await GetNextDepartmentId(), department.Title, department.Description);

            newDepartment = _departmentRepository.Add(newDepartment);
            await UnitOfWork.SaveChangeAsync();

            return _mapper.Map<DepartmentDto>(newDepartment);
        }

        public async Task<DepartmentDto> UpdateDepartmentAsync(int id, DepartmentDto department)
        {
            var updateDepartment = await _departmentRepository.GetAsync(id);

            updateDepartment.Description = department.Description;
            updateDepartment.Title = department.Title;

            _departmentRepository.Update(updateDepartment);
            await UnitOfWork.SaveChangeAsync();

            return _mapper.Map<DepartmentDto>(updateDepartment);
        }

        public async void DeleteDepartmentAsync(int id)
        {
            var deleteDepartment = await _departmentRepository.GetAsync(id);

            if (deleteDepartment != null)
            {
                _departmentRepository.Delete(deleteDepartment);
                await UnitOfWork.SaveChangeAsync();
            }
        }

        private async Task<int> GetNextDepartmentId()
        {
            if (await _departmentRepository.DbSet.CountAsync() == 0)
                return 1;

            return await _departmentRepository.DbSet.MaxAsync(x => x.Id) + 1;
        }
    }
}
