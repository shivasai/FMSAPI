using FMS_Web_Api.DAL;
using FMS_Web_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace FMS_Web_Api.Repository
{
    public interface IFeedbackRepository
    {
        public Task<PostFeedback> SaveFeedbackQuestionAndAnswers(PostFeedback postFeedback);
        public Task<IEnumerable<FeedbackVM>> GetFeedbackQuestions();
        public Task<FeedbackVM> GetFeedbackQuestionById(int id);
        public Task<bool> DeleteFeedbackQuestionById(int id);
        public Task<bool> InsertParticipantFeedbacks(List<ParticipantFeedback> submitFeedbacks);
        public Task<IEnumerable<FeedbackVM>> GetFeedbackQuestionsByParticipantType(PostFeedback postFeedback); 

        public Task<IEnumerable<ParticipantFeedbackVM>> GetParticipantFeedbacksForEvent(int eventId);
        public Task<IEnumerable<ParticipantFeedbackVM>> GetNotParticipatedFeedbacksForEvent(int eventId);
        public Task<IEnumerable<ParticipantFeedbackVM>> GetUnregisteredFeedbacksForEvent(int eventId);
    }
    public class FeedbackRepository : IFeedbackRepository
    {
        public readonly FeedbackQuestionRepository _fbQuestionRepository;
        public readonly FeedbackOptionRepository _fbOptionRepository;
        public readonly ParticipantFeedbackRepository _participantFbRepository;
        public FeedbackRepository(FeedbackQuestionRepository feedbackQuestionRepository, FeedbackOptionRepository feedbackOptionRepository, ParticipantFeedbackRepository participantFeedbackRepository)
        {
            _fbQuestionRepository = feedbackQuestionRepository;
            _fbOptionRepository = feedbackOptionRepository;
            _participantFbRepository = participantFeedbackRepository;
        }

        public async Task<PostFeedback> SaveFeedbackQuestionAndAnswers(PostFeedback postFeedback)
        {
            if(postFeedback.Id == 0)
            {
                return await InsertFeedbackQuestionAndAnswers(postFeedback);
            }
            else
            {
                return await UpdateFeedbackQuestionAndAnswers(postFeedback);
            }
        }
        public async Task<bool> DeleteFeedbackQuestionById(int id)
        {
            var allOptions = await _fbOptionRepository.GetAll();
            var questionOptions = allOptions.Where(x => x.QuestionId == id).ToList();
            foreach (FeedbackOption opt in questionOptions)
            {
                await _fbOptionRepository.Delete(opt.Id);
            }
            await _fbQuestionRepository.Delete(id);
            return true;
        }
        private async Task<PostFeedback> InsertFeedbackQuestionAndAnswers(PostFeedback postFeedback)
        {
            FeedbackQuestion feedbackQuestion = new FeedbackQuestion();
            feedbackQuestion.Question = postFeedback.Question;
            feedbackQuestion.QuestionTye = postFeedback.QuestionTye;
            feedbackQuestion.ParticipantType = postFeedback.ParticipantType;

            feedbackQuestion = await _fbQuestionRepository.Add(feedbackQuestion);

            foreach (string fbOption in postFeedback.FeedbackOptions)
            {

                FeedbackOption option = new FeedbackOption();
                option.Option = fbOption;
                option.QuestionId = feedbackQuestion.Id;
                await _fbOptionRepository.Add(option);
            }
            return postFeedback;
        }

        private async Task<PostFeedback> UpdateFeedbackQuestionAndAnswers(PostFeedback postFeedback)
        {
            FeedbackQuestion feedbackQuestion = new FeedbackQuestion();            
            feedbackQuestion.Question = postFeedback.Question;
            feedbackQuestion.QuestionTye = postFeedback.QuestionTye;
            feedbackQuestion.ParticipantType = postFeedback.ParticipantType;
            feedbackQuestion.Id = postFeedback.Id;

            feedbackQuestion = await _fbQuestionRepository.Update(feedbackQuestion);
            var allOptions = await _fbOptionRepository.GetAll();
            var questionOptions = allOptions.Where(x => x.QuestionId == feedbackQuestion.Id).ToList();
            foreach(FeedbackOption opt in questionOptions)
            {
                await _fbOptionRepository.Delete(opt.Id);
            }
            foreach (string fbOption in postFeedback.FeedbackOptions)
            {

                FeedbackOption option = new FeedbackOption();
                option.Option = fbOption;
                option.QuestionId = feedbackQuestion.Id;
                await _fbOptionRepository.Add(option);
            }
            return postFeedback;
        }

        public async Task<IEnumerable<FeedbackVM>> GetFeedbackQuestions()
        {
            List<FeedbackVM> allQuestions = new List<FeedbackVM>();

            var fbAllQuestions = await _fbQuestionRepository.GetAll();

            foreach (var fbQuestion in fbAllQuestions)
            {
                FeedbackVM feedbackVM = new FeedbackVM();
                feedbackVM.Id = fbQuestion.Id;
                feedbackVM.Question = fbQuestion.Question;
                feedbackVM.ParticipantType = fbQuestion.ParticipantType;
                feedbackVM.QuestionTye = fbQuestion.QuestionTye;

                var questionOptions = await _fbOptionRepository.GetAll();
                int optionCnt = questionOptions.Where(x => x.QuestionId == fbQuestion.Id).Count();
                feedbackVM.OptionsCount = optionCnt;

                allQuestions.Add(feedbackVM);
            }

            return allQuestions;
        }
        public async Task<IEnumerable<FeedbackVM>> GetFeedbackQuestionsByParticipantType(PostFeedback postFeedback)
        {
            List<FeedbackVM> allQuestions = new List<FeedbackVM>();

            var fbAllQuestions = await _fbQuestionRepository.GetAll();
            var participantQns = fbAllQuestions.Where(x => x.ParticipantType == postFeedback.ParticipantType).ToList();
            foreach (var fbQuestion in participantQns)
            {
                FeedbackVM feedbackVM = new FeedbackVM();
                feedbackVM.Id = fbQuestion.Id;
                feedbackVM.Question = fbQuestion.Question;
                feedbackVM.ParticipantType = fbQuestion.ParticipantType;
                feedbackVM.QuestionTye = fbQuestion.QuestionTye;

                var questionOptions = await _fbOptionRepository.GetAll();
                List<FeedbackOption> options = questionOptions.Where(x => x.QuestionId == fbQuestion.Id).ToList();
                feedbackVM.FeedbackOptions = options;

                allQuestions.Add(feedbackVM);
            }

            return allQuestions;
        }
        public async Task<FeedbackVM> GetFeedbackQuestionById(int id)
        {

            FeedbackVM feedbackVM = new FeedbackVM();


            var fbQuestion = await _fbQuestionRepository.Get(id);
            feedbackVM.Id = fbQuestion.Id;
            feedbackVM.Question = fbQuestion.Question;
            feedbackVM.ParticipantType = fbQuestion.ParticipantType;
            feedbackVM.QuestionTye = fbQuestion.QuestionTye;
            var fbOptions = await _fbOptionRepository.GetAll();
            List<FeedbackOption> options = fbOptions.Where(x => x.QuestionId == fbQuestion.Id).ToList();
            feedbackVM.FeedbackOptions = options;

            return feedbackVM;
        }

        // Get Participant feedback
        public async Task<IEnumerable<ParticipantFeedbackVM>> GetParticipantFeedbacksForEvent(int eventId)
        {
            return await GetFeedbacksForEvent(eventId, "Participated");
        }

        //Not participated users feedback
        public async Task<IEnumerable<ParticipantFeedbackVM>> GetNotParticipatedFeedbacksForEvent(int eventId)
        {
            return await GetFeedbacksForEvent(eventId, "NotParticipated");
        }
        //Unregistered users feedback
        public async Task<IEnumerable<ParticipantFeedbackVM>> GetUnregisteredFeedbacksForEvent(int eventId)
        {
            return await GetFeedbacksForEvent(eventId, "Unregistered");
        }
        private async Task<IEnumerable<ParticipantFeedbackVM>> GetFeedbacksForEvent(int eventId, string ParticipantType)
        {
            List<ParticipantFeedbackVM> fbVM = new List<ParticipantFeedbackVM>();

            var allFeedbacks = await _participantFbRepository.GetAll();
            //List<string> participatedEmails = allFeedbacks.Where(x => x.EventId == eventId).Select(x=>x.Email).ToList();
            var distinctEmails = allFeedbacks.GroupBy(test => test.Email)
                        .Select(grp => grp.First());
            foreach (var email in distinctEmails)
            {
                ParticipantFeedbackVM pfbVM = new ParticipantFeedbackVM();
                pfbVM.Feedback = new List<string>();
                var fbs = allFeedbacks.Where(x => x.Email == email.Email && x.ParticipantType == ParticipantType).Select(x => x.Answer).ToList();
                foreach (var fb in fbs)
                {
                    // Answer answer = new Answer();
                    // answer.Ans = fb.ToString();
                    pfbVM.Feedback.Add(fb);
                }
                if (pfbVM.Feedback.Count > 0)
                {
                    fbVM.Add(pfbVM);
                }

            }
            return fbVM;
        }

        public async Task<bool> InsertParticipantFeedbacks(List<ParticipantFeedback> participantFeedbacks)
        {
           foreach(ParticipantFeedback participantFeedback in participantFeedbacks)
            {
                await _participantFbRepository.Add(participantFeedback);
            }           
            return true;
        }
    }
}
