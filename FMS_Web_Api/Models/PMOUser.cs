using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.Models
{
    public class PMOUser
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }
    }
}
