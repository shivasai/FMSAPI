using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FMS_Web_Api.DAL;
using FMS_Web_Api.Models;
using FMS_Web_Api.Repository;
using Microsoft.AspNetCore.Authorization;

namespace FMS_Web_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _repository;

        public EventsController(IEventRepository repository)
        {
            _repository = repository;
        }
        [AllowAnonymous]
        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvent()
        {
            return await _repository.GetAll();
            //return await _context.Events.ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            //var @event = await _context.Events.FindAsync(id);
            var @event = await _repository.Get(id);
            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }
        // GET: api/Events
        [HttpGet()]
        [Route("EventPocDetails/{id}")]
        public async Task<IEnumerable<EventPocDetail>> GetEventPocDetails(int id)
        {
            return await _repository.GetEventPocDetails(id);
            //return await _context.Events.ToListAsync();
        }
        // PUT: api/Events/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEvent(int id, Event @event)
        //{
        //    if (id != @event.Id)
        //    {
        //        return BadRequest();
        //    }            
        //    try
        //    {
        //        await _repository.Update(@event);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        return NotFound();                
        //    }

        //    return Ok();
        //}

        // POST: api/Events
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Event>> PostEvent(Event @event)
        //{
        //    await _repository.Add(@event);
        //    //_context.Events.Add(@event);
        //    //await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        //}

        // DELETE: api/Events/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Event>> DeleteEvent(int id)
        //{
        //    return await _repository.Delete(id);
        //    //var @event = await _context.Events.FindAsync(id);
        //    //if (@event == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //_context.Events.Remove(@event);
        //    //await _context.SaveChangesAsync();

        //    //return @event;
        //}

        //private async bool EventExists(int id)
        //{
        //    Event evnt =  await _repository.Get(id);
        //    if(evnt == null){
        //        return false;
        //    }
        //    return true;
        //    //return _context.Events.Any(e => e.Id == id);
        //}
    }
}
