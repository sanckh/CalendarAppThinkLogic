import { Component, EventEmitter, Input, Output, OnChanges, SimpleChanges } from '@angular/core';
import { EventService } from '../../app/event.service';
import { Event } from '../../app/models/event.model';

@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
})
export class EventFormComponent implements OnChanges {
  @Input() event: Event = this.createDefaultEvent();
  @Output() eventAdded = new EventEmitter<void>();

  constructor(private eventService: EventService) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['event'] && changes['event'].currentValue) {
      this.event = { ...changes['event'].currentValue };
    } else {
      this.event = this.createDefaultEvent();
    }
  }

  addEvent(): void {
    if (this.event.id) {
      this.eventService.updateEvent(this.event).subscribe(() => {
        this.eventAdded.emit();
        this.resetForm();
      });
    } else {
      this.eventService.addEvent(this.event).subscribe(() => {
        this.eventAdded.emit();
        this.resetForm();
      });
    }
  }

  resetForm(): void {
    this.event = this.createDefaultEvent();
  }

  private createDefaultEvent(): Event {
    return {
      id: 0,
      startDate: new Date(),
      endDate: new Date(),
      title: '',
      location: '',
      description: ''
    };
  }
}
