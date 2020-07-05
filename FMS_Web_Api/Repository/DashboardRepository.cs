using FMS_Web_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.Repository
{
    public interface IDashboardRepository
    {
        Task<DashboardVM> Get();
    }
    public class DashboardRepository : IDashboardRepository
    {
        public readonly EventRepository _eventRepository;
        public readonly EventParticipatedUsersRepository _eventParticipatedUserRepository;
        public DashboardRepository(EventRepository eventRepository, EventParticipatedUsersRepository eventParticipatedUsersRepository)
        {
            _eventRepository = eventRepository;
            _eventParticipatedUserRepository = eventParticipatedUsersRepository;
        }
        public async Task<DashboardVM> Get()
        {            
            DashboardVM dashboard = new DashboardVM();
            var events = await _eventRepository.GetAll();            
            dashboard.TotalEvents = events.Count;
            List<EventParticipatedUser> participants = await _eventParticipatedUserRepository.GetAll();
            var distinctRecords = participants.Select(x => x.Email).Distinct();
            dashboard.TotalParticipants = distinctRecords.Count();
            dashboard.TotalVolunteers = events.Sum(x => x.TotalNoOfVolunteers);
            dashboard.LivesImpacted = events.Sum(x => x.LivesImpacted);
            return dashboard;
        }
    }
}
