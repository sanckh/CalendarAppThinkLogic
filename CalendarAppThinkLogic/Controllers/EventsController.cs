using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("{date}")]
        public async Task<ActionResult<IEnumerable<EventModel>>> GetEvents(DateTime date)
        {
            var events = await _eventService.GetEventsAsync(date);
            return Ok(events);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EventModel>> GetEvent(int id)
        {
            var eventModel = await _eventService.GetEventAsync(id);
            if (eventModel == null)
            {
                return NotFound();
            }
            return Ok(eventModel);
        }

        [HttpPost]
        public async Task<ActionResult<EventModel>> PostEvent(EventModel eventModel)
        {
            var createdEvent = await _eventService.AddEventAsync(eventModel);
            return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.Id }, createdEvent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, EventModel eventModel)
        {
            try
            {
                await _eventService.UpdateEventAsync(id, eventModel);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
