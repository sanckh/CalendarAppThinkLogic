import { Component, OnInit } from '@angular/core';
import { EventService } from '../../app/event.service';
import { Event } from '../../app/models/event.model';
import { CalendarOptions } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
})
export class CalendarComponent implements OnInit {
  events: Event[] = [];
  selectedDate: Date = new Date();
  selectedEvent: Event | null = null;

  calendarOptions: CalendarOptions = {
    initialView: 'dayGridMonth',
    plugins: [dayGridPlugin, interactionPlugin],
    dateClick: this.handleDateClick.bind(this),
    events: []
  };

  constructor(private eventService: EventService) { }

  ngOnInit(): void {
    this.loadEvents(this.selectedDate);
  }

  loadEvents(date: Date): void {
    const dateString = date.toISOString().split('T')[0];
    this.eventService.getEvents(dateString).subscribe(events => {
      this.events = events;
      this.calendarOptions.events = events.map(event => ({
        title: event.title,
        start: event.startDate,
        end: event.endDate,
        extendedProps: {
          location: event.location,
          description: event.description
        }
      }));
    });
  }

  handleDateClick(arg: any) {
    this.selectedDate = new Date(arg.date);
    this.loadEvents(this.selectedDate);
  }

  handleEventEdited(event: Event): void {
    this.selectedEvent = event;
  }
}
