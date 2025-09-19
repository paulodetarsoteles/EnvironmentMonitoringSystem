import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environment';

export interface BaseResponse<T> {
    success: boolean;
    errorMessages: string[];
    data: T;
}

export interface DeviceListResponse {
    id: string;
    deviceName: string;
    location: string;
}

export interface Device {
    id: string;
    deviceName: string;
    location: string;
    creationDate: Date;
    lastUpdate?: Date | null;
}

export interface DeviceAddRequest {
    deviceName: string;
    location: string;
}

@Injectable({
    providedIn: 'root'
})
export class DevicesService {
    private apiUrl = `${environment.apiUrl}/devices`;

    constructor(private http: HttpClient) { }

    listDevices(): Observable<BaseResponse<DeviceListResponse[]>> {
        return this.http.get<BaseResponse<DeviceListResponse[]>>(this.apiUrl);
    }

    getDeviceById(id: string): Observable<BaseResponse<Device>> {
        return this.http.get<BaseResponse<Device>>(`${this.apiUrl}/${id}`);
    }

    addDevice(request: DeviceAddRequest): Observable<any> {
        return this.http.post(`${environment.apiUrl}/devices`, request);
    }

    updateDevice(id: string, request: DeviceAddRequest) {
        return this.http.put(`${this.apiUrl}/${id}`, request);
    }

    deleteDevice(id: string) {
        return this.http.delete(`${this.apiUrl}/${id}`);
    }
}
