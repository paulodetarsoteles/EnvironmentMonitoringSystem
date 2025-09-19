import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { environment } from '../../environment';

export interface Event {
    deviceId: string;
    timeStamp: Date | null;
    temperature: number;
    humidity: number;
    isAlarm: boolean;
}

@Injectable({
    providedIn: 'root'
})
export class EventsService {
    private hubConnection!: HubConnection;
    private eventsSubject = new BehaviorSubject<Event[]>([]);
    public events$ = this.eventsSubject.asObservable();

    private apiUrl = `${environment.apiUrl}`;

    constructor() {
        this.startConnection();
    }

    private startConnection() {
        this.hubConnection = new HubConnectionBuilder()
            .withUrl(`${this.apiUrl}/hubs/events`)
            .withAutomaticReconnect()
            .build();

        this.hubConnection
            .start()
            .then(() => console.log('SignalR connected'))
            .catch(err => console.error('SignalR connection error: ', err));

        this.hubConnection.on('ReceiveEventList', (events: any[]) => {
            const parsedEvents: Event[] = events.map(e => ({
                deviceId: e.data.deviceId,
                temperature: e.data.temperature,
                humidity: e.data.humidity,
                isAlarm: e.data.isAlarm,
                timeStamp: e.data.timeStamp ? new Date(e.data.timeStamp) : null
            }));

            this.eventsSubject.next(parsedEvents);
        });

    }
}

