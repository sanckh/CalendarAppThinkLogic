using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventModel>> GetEventsAsync(DateTime date);
        Task<EventModel> GetEventAsync(int id);
        Task<EventModel> AddEventAsync(EventModel eventModel);
        Task UpdateEventAsync(int id, EventModel eventModel);
    }
}
