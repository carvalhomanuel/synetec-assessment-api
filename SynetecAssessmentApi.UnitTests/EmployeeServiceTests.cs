using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SynetecAssessmentApi.Application.RequestModels;
using SynetecAssessmentApi.Application.Services.Interfaces;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence.Repository;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.UnitTests
{
    public class EmployeeServiceTests : TestsBase
    {
        protected IEmployeeService EmployeeService { get; set; }
       
        [SetUp]
        public void Setup()
        {
            ConfigureServiceProvider();

            EmployeeService = ServicesProvider.GetService<IEmployeeService>();
        }

        [Test]
        public void Employee_Service_Should_Get_Resolved()
        {
            Assert.IsNotNull(EmployeeService);
        }

        [Test]
        public void Employee_Repository_Should_Get_Resolved()
        {
            var repository = ServicesProvider.GetService<IRepository<Employee>>();

            Assert.IsNotNull(repository);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        public void Get_Existent_Employee_Should_Be_Not_Null(int employeeId)
        {
            var employee = EmployeeService.GetEmployeeAsync(employeeId);

            Assert.IsNotNull(employee);
        }

        [Test]
        public void Add_New_Employee_Should_Exist_After_Added()
        {
            EmployeeRequestModel employeeRM = new EmployeeRequestModel()
            {
                DepartmentId = 3,
                Fullname = "Manuel Carvalho",
                JobTitle = "Senior Software Developer",
                Salary = 80000
            };

            var newEmployee = EmployeeService.SaveEmployeeAsync(employeeRM);

            Assert.IsNotNull(newEmployee);
        }

        [Test]
        public async Task Update_Employee_Should_Be_Updated()
        {
            EmployeeRequestModel employeeRM = new EmployeeRequestModel()
            {
                DepartmentId = 3,
                Fullname = "John Doe",
                JobTitle = "Team Lead",
                Salary = 100000
            };

            var employee = EmployeeService.UpdateEmployeeAsync(1, employeeRM);

            Assert.IsNotNull(employee);

            var updatedEmployee = await EmployeeService.GetEmployeeAsync(1);

            Assert.IsNotNull(updatedEmployee);
            Assert.AreEqual(updatedEmployee.Fullname, employeeRM.Fullname);
            Assert.AreEqual(updatedEmployee.JobTitle, employeeRM.JobTitle);
            Assert.AreEqual(updatedEmployee.Salary, employeeRM.Salary);
        }

        [Test]
        public async Task Deleted_Employee_Should_Not_Exist()
        {
            EmployeeService.DeleteEmployeeAsync(12);

            var deletedEmployee = await EmployeeService.GetEmployeeAsync(12);

            Assert.IsNull(deletedEmployee);
        }
    }
}