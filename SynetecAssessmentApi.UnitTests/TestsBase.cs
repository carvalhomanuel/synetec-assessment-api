using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SynetecAssessmentApi.Application.Mapper;
using SynetecAssessmentApi.Application.Services;
using SynetecAssessmentApi.Application.Services.Interfaces;
using SynetecAssessmentApi.Persistence;
using SynetecAssessmentApi.Persistence.Infrastructure;
using SynetecAssessmentApi.Persistence.Repository;
using System;

namespace SynetecAssessmentApi.UnitTests
{
    public class TestsBase
    {
        protected IServiceProvider ServicesProvider { get; set; }

        protected void ConfigureServiceProvider()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            ServicesProvider = services.BuildServiceProvider();
            DbContextGenerator.Initialize(ServicesProvider);
        }

        protected void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
               options.UseInMemoryDatabase(databaseName: "HrDb"));

            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IDepartmentService, DepartmentService>()
                .AddScoped<IEmployeeService, EmployeeService>()
                .AddScoped<IBonusPoolService, BonusPoolService>();

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
