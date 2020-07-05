using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.Models
{
    public class FeedbackQuestion : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string QuestionTye { get; set; }
        public string Question { get; set; }
        public string ParticipantType { get; set; }
    }
}
