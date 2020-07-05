using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.Models
{
    public class FeedbackVM
    {
        public int Id { get; set; }
        public string QuestionTye { get; set; }
        public string Question { get; set; }
        public string ParticipantType { get; set; }
        public int OptionsCount { get; set; }
        public List<FeedbackOption> FeedbackOptions { get; set; }
    }
}
