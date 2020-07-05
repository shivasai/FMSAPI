using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.Models
{
    public class PostFeedback
    {
        public int Id { get; set; }
        public string QuestionTye { get; set; }
        public string Question { get; set; }
        public string ParticipantType { get; set; }
        public int OptionsCount { get; set; }
        public string Answer { get; set; }
        public List<string> FeedbackOptions { get; set; }
    }
}
