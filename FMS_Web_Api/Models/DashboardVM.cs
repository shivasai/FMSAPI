using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.Models
{
    public class DashboardVM
    {
        public int TotalEvents { get; set; }
        public int LivesImpacted { get; set; }
        public int TotalVolunteers { get; set; }
        public int TotalParticipants { get; set; }
    }
}
