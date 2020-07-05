using FMS_Web_Api.Controllers;
using FMS_Web_Api.Models;
using FMS_Web_Api.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FMS_Web_Api.Tests
{
    
    class FeedbackTests
    {
        private FeedbackController feedbackController;
        private readonly Mock<IFeedbackRepository> _feedbackRepository = new Mock<IFeedbackRepository>();
        [SetUp]
        public void Setup()
        {
            feedbackController = new FeedbackController(_feedbackRepository.Object);
        }
        [Test]
        public async Task GetFeedbackQnsAsync_ShouldReturnFeedbackQns_FeedbackQnExists()
        {           
            List<FeedbackVM> feedbackVMs = new List<FeedbackVM>();
            FeedbackVM feedback = new FeedbackVM()
            {
                Id = 1,
                Question = "How is the event?",
                QuestionTye = "MultipleAnswer"
            };
            feedbackVMs.Add(feedback);
            _feedbackRepository.Setup(x => x.GetFeedbackQuestions()).ReturnsAsync(feedbackVMs);

            IEnumerable<FeedbackVM> result = await feedbackController.Get();
            Assert.IsNotEmpty(result);
           
        }
        [Test]
        public async Task GetParticipantFbAsync_ShouldReturnParticipantFb_ParticipantFbExists()
        {
            int id = 1;
            List<ParticipantFeedbackVM> feedbackVMs = new List<ParticipantFeedbackVM>();
            ParticipantFeedbackVM feedback = new ParticipantFeedbackVM();
            List<string> fbNames = new List<string>();
            fbNames.Add("Good Event");
            fbNames.Add("Excellent");
            feedback.Feedback = fbNames;


            feedbackVMs.Add(feedback);
            _feedbackRepository.Setup(x => x.GetParticipantFeedbacksForEvent(id)).ReturnsAsync(feedbackVMs);

            IEnumerable<ParticipantFeedbackVM> result = await feedbackController.GetParticipantFeedbacks(id);
            Assert.IsNotEmpty(result);

        }

        [Test]
        public async Task GetNotParticipantFbAsync_ShouldReturnNotParticipantFb_NotParticipantFbExists()
        {
            int id = 1;
            List<ParticipantFeedbackVM> feedbackVMs = new List<ParticipantFeedbackVM>();
            ParticipantFeedbackVM feedback = new ParticipantFeedbackVM();
            List<string> fbNames = new List<string>();
            fbNames.Add("Incorrectly registered");
            fbNames.Add("Unexpected");
            feedback.Feedback = fbNames;


            feedbackVMs.Add(feedback);
            _feedbackRepository.Setup(x => x.GetNotParticipatedFeedbacksForEvent(id)).ReturnsAsync(feedbackVMs);

            IEnumerable<ParticipantFeedbackVM> result = await feedbackController.GetNotParticipatedFeedbacks(id);
            Assert.IsNotEmpty(result);

        }
        [Test]
        public async Task GetunregisteredFbAsync_ShouldReturnunregisteredFb_unregisteredFbExists()
        {
            int id = 1;
            List<ParticipantFeedbackVM> feedbackVMs = new List<ParticipantFeedbackVM>();
            ParticipantFeedbackVM feedback = new ParticipantFeedbackVM();
            List<string> fbNames = new List<string>();
            fbNames.Add("Incorrectly registered");            
            feedback.Feedback = fbNames;


            feedbackVMs.Add(feedback);
            _feedbackRepository.Setup(x => x.GetUnregisteredFeedbacksForEvent(id)).ReturnsAsync(feedbackVMs);

            IEnumerable<ParticipantFeedbackVM> result = await feedbackController.GetUnregisteredFeedbacks(id);
            Assert.IsNotEmpty(result);

        }
    }
}
