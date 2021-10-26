using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SynetecAssessmentApi.Application.Dtos;
using SynetecAssessmentApi.Application.Services.Interfaces;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence.Repository;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.UnitTests
{
    public class DepartmentServiceTests : TestsBase
    {
        protected IDepartmentService DepartmentService { get; set; }
       
        [SetUp]
        public void Setup()
        {
            ConfigureServiceProvider();

            DepartmentService = ServicesProvider.GetService<IDepartmentService>();
        }

        [Test]
        public void Department_Service_Should_Get_Resolved()
        {
            Assert.IsNotNull(DepartmentService);
        }

        [Test]
        public void Department_Repository_Should_Get_Resolved()
        {
            var repository = ServicesProvider.GetService<IRepository<Department>>();

            Assert.IsNotNull(repository);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void Get_Existent_Department_Should_Be_Not_Null(int departmentId)
        {
            var department = DepartmentService.GetDepartmentAsync(departmentId);

            Assert.IsNotNull(department);
        }

        [Test]
        public void Add_New_Department_Should_Exist_After_Added()
        {
            DepartmentDto newDepartment = new DepartmentDto()
            {
                Description = "The development department for the company",
                Title = "Development"
            };

            var department = DepartmentService.SaveDepartmentAsync(newDepartment);

            Assert.IsNotNull(department);
        }

        [Test]
        public async Task Update_Department_Should_Be_Updated()
        {
            DepartmentDto departmentDto = new DepartmentDto()
            {
                Description = "The sales department for the company",
                Title = "Sales"
            };

            var department = DepartmentService.UpdateDepartmentAsync(1, departmentDto);

            Assert.IsNotNull(department);

            var updatedDepartment = await DepartmentService.GetDepartmentAsync(1);

            Assert.IsNotNull(updatedDepartment);
            Assert.AreEqual(updatedDepartment.Title, departmentDto.Title);
            Assert.AreEqual(updatedDepartment.Description, departmentDto.Description);
        }

        [Test]
        public async Task Deleted_Department_Should_Not_Exist()
        {
            DepartmentService.DeleteDepartmentAsync(4);

            var deletedDepartment = await DepartmentService.GetDepartmentAsync(4);

            Assert.IsNull(deletedDepartment);
        }
    }
}