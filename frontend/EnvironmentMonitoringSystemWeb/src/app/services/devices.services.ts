import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environment';

export interface DeviceListResponse {
  deviceName: string;
  location: string;
}

export interface BaseResponse<T> {
  success: boolean;
  errorMessages: string[];
  data: T;
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

  constructor(private http: HttpClient) {}

  listDevices(): Observable<BaseResponse<DeviceListResponse[]>> {
    return this.http.get<BaseResponse<DeviceListResponse[]>>(this.apiUrl);
  }

  addDevice(request: DeviceAddRequest): Observable<any> {
    console.log(`${environment.apiUrl}/devices`);
    console.log(request);
  return this.http.post(`${environment.apiUrl}/devices`, request);
}
}
