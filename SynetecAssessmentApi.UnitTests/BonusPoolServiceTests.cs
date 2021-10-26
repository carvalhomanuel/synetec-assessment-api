using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SynetecAssessmentApi.Application.Services.Interfaces;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence.Repository;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.UnitTests
{
    public class BonusPoolServiceTests : TestsBase
    {
        protected IBonusPoolService BonusPoolService { get; set; }

        [SetUp]
        public void Setup()
        {
            ConfigureServiceProvider();

            BonusPoolService = ServicesProvider.GetService<IBonusPoolService>();
        }

        [Test]
        public void BonusPool_Service_Should_Get_Resolved()
        {
            Assert.IsNotNull(BonusPoolService);
        }

        [Test]
        public void Employee_Repository_Should_Get_Resolved()
        {
            var repository = ServicesProvider.GetService<IRepository<Employee>>();

            Assert.IsNotNull(repository);
        }

        [Test]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(0, 3, 0)]
        [TestCase(0, 4, 0)]
        [TestCase(0, 5, 0)]
        [TestCase(0, 6, 0)]
        [TestCase(0, 7, 0)]
        [TestCase(0, 8, 0)]
        [TestCase(0, 9, 0)]
        [TestCase(0, 10, 0)]
        [TestCase(0, 11, 0)]
        [TestCase(0, 12, 0)]
        [TestCase(5000, 1, 458)]
        [TestCase(5000, 2, 687)]
        [TestCase(5000, 3, 725)]
        [TestCase(5000, 4, 420)]
        [TestCase(5000, 5, 343)]
        [TestCase(5000, 6, 267)]
        [TestCase(5000, 7, 477)]
        [TestCase(5000, 8, 295)]
        [TestCase(5000, 9, 274)]
        [TestCase(5000, 10, 278)]
        [TestCase(5000, 11, 404)]
        [TestCase(5000, 12, 366)]
        public async Task ExpectedBonusAllocation_Should_Be_Correct(int bonusPoolAmount, int employeeId, int expectedBonusAllocation)
        {
            var employee = await BonusPoolService.CalculateAsync(bonusPoolAmount, employeeId);

            Assert.AreEqual(employee.Amount, expectedBonusAllocation);
        }
    }
}