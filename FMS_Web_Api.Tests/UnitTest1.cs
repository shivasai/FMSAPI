using FMS_Web_Api.Controllers;
using FMS_Web_Api.DAL;
using FMS_Web_Api.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace FMS_Web_Api.Tests
{
    public class Tests
    {
        //DashboardController dashboardController;
        //private DashboardRepository _dashboardRepository;
        //private Mock<IEventRepository> _eventRepository;
        //private Mock<IEventParticipatedUsersRepository> _eventParticipatedUserRepository;
        //private Mock<ApplicationDbContext> _applicationDbContext;
        //private Mock<DbContextOptions> _dbContextOptions;
        [SetUp]
        public void Setup()
        {
            
            //dashboardController = new DashboardController(_dashboardRepository.Object);
        }

        //[Test]
        //public void Test1()
        //{

             
        //   // var res = dashboardController.Get();

        //    Assert.Pass();
        //}

        //[Test]
        //public void Dashboard()
        //{
            
        //    _eventRepository = new Mock<IEventRepository>(MockBehavior.Strict);
        //    _eventParticipatedUserRepository = new Mock<IEventParticipatedUsersRepository>(MockBehavior.Strict);

        //    _dashboardRepository = new DashboardRepository(_eventRepository, _eventParticipatedUserRepository);

        //    Assert.Pass();
        //}

        //[TestCase(675, "We look forward to doing business with you!")]
        //public void MakeCreditDecision_Always_ReturnsExpectedResult(int creditScore, string expectedResult)
        //{
        //    mockCreditDecisionService = new Mock<ICreditDecisionService>(MockBehavior.Strict);
        //    mockCreditDecisionService.Setup(p => p.GetDecision(creditScore)).Returns(expectedResult);


        //    systemUnderTest = new CreditDecision(mockCreditDecisionService.Object);
        //    var result = systemUnderTest.MakeCreditDecision(creditScore);

        //    Assert.That(result, Is.EqualTo(expectedResult));

        //    mockCreditDecisionService.VerifyAll();
        //}
    }
}