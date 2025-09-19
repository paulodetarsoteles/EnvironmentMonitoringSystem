import { Component, OnInit } from '@angular/core';
import { EventsService, Event } from '../../services/events.service';
import { CommonModule, DatePipe } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    DatePipe
  ],
  templateUrl: './home.html'
})
export class Home implements OnInit {
  events: Event[] = [];
  displayedColumns: string[] = ['deviceId', 'temperature', 'humidity', 'timeStamp', 'isAlarm'];

  constructor(private eventsService: EventsService) { }

  ngOnInit(): void {
    this.eventsService.events$.subscribe(evts => {
      console.log('Eventos recebidos:', evts);
      this.events = evts.map(e => ({
        ...e,
        timeStamp: e.timeStamp ? new Date(e.timeStamp) : null
      }));
    });
  }
}
