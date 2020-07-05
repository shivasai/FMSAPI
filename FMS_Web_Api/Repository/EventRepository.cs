using FMS_Web_Api.DAL;
using FMS_Web_Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.Repository
{
    public interface IEventRepository
    {
        public Task<IEnumerable<EventPocDetail>> GetEventPocDetails(int EventId);
        public Task<List<Event>> GetAll();

        public Task<Event> Get(int id);
        public Task<Event> Add(Event entity);
        public Task<Event> Delete(int id);
        public Task<Event> Update(Event entity);
    }
    public class EventRepository : IEventRepository //: EfCoreRepository<Event, ApplicationDbContext>
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext context) //: base(context)
        {
            _context = context;
        }
        public async Task<Event> Add(Event entity)
        {
            _context.Set<Event>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Event> Delete(int id)
        {
            var entity = await _context.Set<Event>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Set<Event>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Event> Get(int id)
        {
            return await _context.Set<Event>().FindAsync(id);
        }

        public async Task<List<Event>> GetAll()
        {
            return await _context.Set<Event>().ToListAsync();
        }

        public async Task<Event> Update(Event entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        
        public async Task<IEnumerable<EventPocDetail>> GetEventPocDetails(int EventId)
        {
            return await _context.EventPocDetails.Where(x => x.EventId == EventId).ToListAsync();
        }
       
    }
}
