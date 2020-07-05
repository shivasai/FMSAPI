using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using FMS_Web_Api.Controllers;
using FMS_Web_Api.Repository;
using System.Threading.Tasks;
using FMS_Web_Api.Models;
using NuGet.Frameworks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Results;

namespace FMS_Web_Api.Tests
{
    public class EventTests
    {
        private EventsController eventsController;
        private readonly Mock<IEventRepository> _eventRepository = new Mock<IEventRepository>();
        [SetUp]
        public void Setup()
        {
            eventsController = new EventsController(_eventRepository.Object);
        }
        [Test]
        public async Task GetEventsAsync_ShouldReturnEvents_WhenEventsExists()
        {
            List<Event> events = new List<Event>();
            Event evnt = new Event()
            {
                EventId = "2",
                EventName = "Test"
            };
            events.Add(evnt);
            _eventRepository.Setup(x => x.GetAll()).ReturnsAsync(events);

            var actualevents = await eventsController.GetEvent();
            Assert.AreEqual(actualevents.Value.Count(), 1);
        }
        [Test]
        public async Task GetEventAsync_ShouldReturnEvents_WhenEventExists()
        {
            int id = 2;
            Event evnt = new Event()
            {
                EventId = "2",
                EventName = "Test"
            };
            
            _eventRepository.Setup(x => x.Get(id)).ReturnsAsync(evnt);

            var result = await eventsController.GetEvent(id);
            Assert.AreEqual(result.Value.EventName, "Test");
        }
        [Test]
        public async Task GetEventPocDetailsAsync_ShouldReturnEventPocDetails_EventPocDetailsExists()
        {

            int id = 1;
            List<EventPocDetail> eventPocDetails = new List<EventPocDetail>();
            EventPocDetail eventPocDetail1 = new EventPocDetail()
            {
                Id = 1,
                Name = "Shiva"
            };
            EventPocDetail eventPocDetail2 = new EventPocDetail()
            {
                Id = 2,
                Name = "Sai"
            };
            eventPocDetails.Add(eventPocDetail1);
            eventPocDetails.Add(eventPocDetail2);
            _eventRepository.Setup(x => x.GetEventPocDetails(id)).ReturnsAsync(eventPocDetails);

            var result = await eventsController.GetEventPocDetails(id);
            Assert.AreEqual(result.ToList().Count(), 2);
        }
      
    }
}
