import { Component, Input } from '@angular/core';
import { Event } from '../../app/models/event.model';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
})
export class EventListComponent {
  @Input() events: Event[] = [];
}
