using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FMS_Web_Api.Models;
using FMS_Web_Api.Controllers;
using FMS_Web_Api.Repository;

namespace FMS_Web_Api.Tests
{
    class DashboardTests
    {
        private DashboardController dashboardController;
        private readonly Mock<IDashboardRepository> _dashboardRepository = new Mock<IDashboardRepository>();
        [SetUp]
        public void Setup()
        {
            dashboardController = new DashboardController(_dashboardRepository.Object);
        }

        [Test]
        public async Task GetDashboardAsync_ShouldReturnDashboard_WhenDashboardExists()
        {
            int id = 2;
            DashboardVM dashboard = new DashboardVM()
            {
                TotalEvents = 8,
                LivesImpacted = 877,
                TotalVolunteers = 56,
                TotalParticipants = 6
            };

            _dashboardRepository.Setup(x => x.Get()).ReturnsAsync(dashboard);

            var result = await dashboardController.Get();
            Assert.AreEqual(result.Value.TotalEvents, 8);
            Assert.AreEqual(result.Value.LivesImpacted, 877);
            Assert.AreEqual(result.Value.TotalVolunteers, 56);
            Assert.AreEqual(result.Value.TotalParticipants, 6);
        }
    }
}
