using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS_Web_Api.Models;
using FMS_Web_Api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS_Web_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _repository;
        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            _repository = feedbackRepository;
        }
        [AllowAnonymous]
        // GET: api/<FeedbackController>
        [HttpGet]
        public async Task<IEnumerable<FeedbackVM>> Get()
        {
            
            return await _repository.GetFeedbackQuestions();
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("FeedbacksByParticipants")]
        public async Task<IEnumerable<FeedbackVM>> PostFeedbackQuestionsByParticipantType([FromBody] PostFeedback postFeedback)
        {
            return await _repository.GetFeedbackQuestionsByParticipantType(postFeedback);
            
        }
        [HttpGet("{id}")]
        public async Task<FeedbackVM> Get(int id)
        {
            return await _repository.GetFeedbackQuestionById(id);
        }
        [HttpPost]
        public async Task<PostFeedback> Post([FromBody] PostFeedback postFeedback)
        {
            return await _repository.SaveFeedbackQuestionAndAnswers(postFeedback);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("ParticipantFeedbacks")]
        public async Task<ActionResult> PostParticipantFeedback([FromBody] List<ParticipantFeedback> participantFeedbacks)
        {
            await _repository.InsertParticipantFeedbacks(participantFeedbacks);
            // return await _repository.SaveFeedbackQuestionAndAnswers(postFeedback);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteFeedbackQuestionById(id);
            return Ok();
        }
        [HttpGet]
        [Route("ParticipantFeedbacks/{id}")]
        public async Task<IEnumerable<ParticipantFeedbackVM>> GetParticipantFeedbacks(int id)
        {
            return await _repository.GetParticipantFeedbacksForEvent(id);
        }
        [HttpGet]
        [Route("NotParticipatedFeedbacks/{id}")]
        public async Task<IEnumerable<ParticipantFeedbackVM>> GetNotParticipatedFeedbacks(int id)
        {
            return await _repository.GetNotParticipatedFeedbacksForEvent(id);
        }
        [HttpGet]
        [Route("UnregisteredFeedbacks/{id}")]
        public async Task<IEnumerable<ParticipantFeedbackVM>> GetUnregisteredFeedbacks(int id)
        {
            return await _repository.GetUnregisteredFeedbacksForEvent(id);
        }
    }
}
