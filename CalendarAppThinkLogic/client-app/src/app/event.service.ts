import { environment } from 'src/environments/environment';
import { Event } from '../app/models/event.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private apiUrl = `${environment.apiUrl}/events`;

  constructor(private http: HttpClient) { }

  getEvents(date: string): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.apiUrl}/${date}`);
  }

  getEvent(id: number): Observable<Event> {
    return this.http.get<Event>(`${this.apiUrl}/${id}`);
  }

  addEvent(event: Event): Observable<Event> {
    return this.http.post<Event>(this.apiUrl, event);
  }

  updateEvent(event: Event): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${event.id}`, event);
  }
}
