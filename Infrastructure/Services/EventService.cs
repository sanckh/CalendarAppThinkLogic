using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventModel>> GetEventsAsync(DateTime date)
        {
            var events = await _context.Events
                .Where(e => e.StartDate.Date == date.Date)
                .ToListAsync();

            return events.Select(e => new EventModel
            {
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Title = e.Title,
                Location = e.Location,
                Description = e.Description
            });
        }

        public async Task<EventModel> GetEventAsync(int id)
        {
            var eventEntity = await _context.Events.FindAsync(id);

            if (eventEntity == null)
            {
                return null;
            }

            return new EventModel
            {
                StartDate = eventEntity.StartDate,
                EndDate = eventEntity.EndDate,
                Title = eventEntity.Title,
                Location = eventEntity.Location,
                Description = eventEntity.Description
            };
        }

        public async Task<EventModel> AddEventAsync(EventModel eventModel)
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

            eventModel.Id = eventEntity.Id;
            return eventModel;
        }

        public async Task UpdateEventAsync(int id, EventModel eventModel)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity == null)
            {
                throw new KeyNotFoundException("Event not found.");
            }

            eventEntity.StartDate = eventModel.StartDate;
            eventEntity.EndDate = eventModel.EndDate;
            eventEntity.Title = eventModel.Title;
            eventEntity.Location = eventModel.Location;
            eventEntity.Description = eventModel.Description;

            _context.Entry(eventEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
