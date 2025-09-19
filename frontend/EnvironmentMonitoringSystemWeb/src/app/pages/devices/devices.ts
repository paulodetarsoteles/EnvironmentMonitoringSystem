import { Component, OnInit } from '@angular/core';
import { DevicesService, DeviceListResponse, DeviceAddRequest } from '../../services/devices.services';
import { NgIf } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-devices',
  standalone: true,
  templateUrl: './devices.html',
  imports: [NgIf, MatButtonModule, MatTableModule]
})
export class Devices implements OnInit {
  devices: DeviceListResponse[] = [];
  errorMessage: string | null = null;

  displayedColumns: string[] = ['deviceName', 'location'];

  constructor(private devicesService: DevicesService) {}

  ngOnInit(): void {
    this.loadDevices();
  }

  loadDevices() {
    this.devicesService.listDevices().subscribe({
      next: (res) => {
        this.devices = res.data || [];
        this.errorMessage = this.devices.length === 0 ? 'Nenhum dispositivo encontrado.' : null;
      },
      error: (err) => {
        this.devices = [];
        this.errorMessage = 'Erro de comunicação com a API.';
        alert(this.errorMessage);
        console.error(err);
      }
    });
  }

  addDevice() {
    const newDevice: DeviceAddRequest = {
      deviceName: 'Novo Dispositivo',
      location: 'Local Padrão'
    };

    this.devicesService.addDevice(newDevice).subscribe({
      next: () => {
        alert('Dispositivo adicionado com sucesso!');
        this.loadDevices(); // recarrega a lista
      },
      error: (err) => {
        alert('Falha ao adicionar dispositivo.');
        console.error(err);
      }
    });
  }
}
