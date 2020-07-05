using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.Models
{
    public class FeedbackOption: IEntity
    {
        [Key]
        public int Id { get; set; }

        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public FeedbackQuestion FeedbackQuestion { get; set; }
        public string Option { get; set; }
    }
}
