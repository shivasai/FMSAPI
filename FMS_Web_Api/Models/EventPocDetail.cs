﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.Models
{
    public class EventPocDetail
    {
        [Key]
        public int Id { get; set; }        
        public int EventId { get; set; }      
        public string EmpId { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
    }
}
