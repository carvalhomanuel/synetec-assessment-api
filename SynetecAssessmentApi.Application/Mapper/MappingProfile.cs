using AutoMapper;
using SynetecAssessmentApi.Application.Dtos;
using SynetecAssessmentApi.Domain;

namespace SynetecAssessmentApi.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();

            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
        }
    }
}
