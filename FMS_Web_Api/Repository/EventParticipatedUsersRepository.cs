using FMS_Web_Api.DAL;
using FMS_Web_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.Repository
{
    public class EventParticipatedUsersRepository : EfCoreRepository<EventParticipatedUser, ApplicationDbContext>
    {
        public EventParticipatedUsersRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
