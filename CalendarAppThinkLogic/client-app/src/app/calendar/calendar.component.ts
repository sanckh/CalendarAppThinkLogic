import { Component, OnInit } from '@angular/core';
import { EventService } from '../../app/event.service';
import { Event } from '../../app/models/event.model';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {
  events: Event[] = [];
  selectedDate: Date = new Date();

  constructor(private eventService: EventService) { }

  ngOnInit(): void {
    this.loadEvents(this.selectedDate);
  }

  loadEvents(date: Date): void {
    const dateString = date.toISOString().split('T')[0];
    this.eventService.getEvents(dateString).subscribe(events => {
      this.events = events;
    });
  }

  onDateChange(event: any): void {
    const input = event.target as HTMLInputElement;
    this.selectedDate = input.valueAsDate || new Date(); // default to today's date if null
    this.loadEvents(this.selectedDate);
  }
}
