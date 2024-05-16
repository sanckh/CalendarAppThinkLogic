using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{date}")]
        public async Task<ActionResult<IEnumerable<EventModel>>> GetEvents(DateTime date)
        {
            var events = await _context.Events
                .Where(e => e.StartDate.Date == date.Date)
                .ToListAsync();

            var eventModels = events.Select(e => new EventModel
            {
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Title = e.Title,
                Location = e.Location,
                Description = e.Description
            });

            return Ok(eventModels);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EventModel>> GetEvent(int id)
        {
            var eventEntity = await _context.Events.FindAsync(id);

            if (eventEntity == null)
            {
                return NotFound();
            }

            var eventModel = new EventModel
            {
                StartDate = eventEntity.StartDate,
                EndDate = eventEntity.EndDate,
                Title = eventEntity.Title,
                Location = eventEntity.Location,
                Description = eventEntity.Description
            };

            return Ok(eventModel);
        }

        [HttpPost]
        public async Task<ActionResult<EventModel>> PostEvent(EventModel eventModel)
        {
            var eventEntity = new Event
            {
                StartDate = eventModel.StartDate,
                EndDate = eventModel.EndDate,
                Title = eventModel.Title,
                Location = eventModel.Location,
                Description = eventModel.Description
            };

            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvent), new { id = eventEntity.Id }, eventModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, EventModel eventModel)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity == null)
            {
                return NotFound();
            }

            eventEntity.StartDate = eventModel.StartDate;
            eventEntity.EndDate = eventModel.EndDate;
            eventEntity.Title = eventModel.Title;
            eventEntity.Location = eventModel.Location;
            eventEntity.Description = eventModel.Description;

            _context.Entry(eventEntity).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Events.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
