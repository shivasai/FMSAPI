using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.Models
{
    public class ParticipantFeedback : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int EventId { get; set; }
        public string Email { get; set; }
        public string ParticipantType { get; set; }

        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
