import { Component, EventEmitter, Output } from '@angular/core';
import { EventService } from '../../app/event.service';
import { Event } from '../../app/models/event.model';

@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
})
export class EventFormComponent {
  @Output() eventAdded = new EventEmitter<void>();

  event: Event = {
    id: 0,
    startDate: new Date(),
    endDate: new Date(),
    title: '',
    location: '',
    description: ''
  };

  constructor(private eventService: EventService) { }

  addEvent(): void {
    this.eventService.addEvent(this.event).subscribe(() => {
      this.eventAdded.emit();
      this.resetForm();
    });
  }

  resetForm(): void {
    this.event = {
      id: 0,
      startDate: new Date(),
      endDate: new Date(),
      title: '',
      location: '',
      description: ''
    };
  }
}
