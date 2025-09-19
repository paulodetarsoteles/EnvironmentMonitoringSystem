import { Component, OnInit } from '@angular/core';
import { DevicesService, DeviceListResponse, DeviceAddRequest, Device } from '../../services/devices.services';
import { NgIf } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { DeviceDialog, DeviceDialogData } from './device-dialog';
import { DeviceViewDialog } from './device-view-dialog';

@Component({
  selector: 'app-devices',
  standalone: true,
  templateUrl: './devices.html',
  styleUrls: ['./devices.css'],
  imports: [NgIf, MatTableModule, MatButtonModule, MatDialogModule]
})
export class Devices implements OnInit {
  devices: DeviceListResponse[] = [];
  errorMessage: string | null = null;

  displayedColumns: string[] = ['id', 'deviceName', 'location', 'actions'];

  constructor(private devicesService: DevicesService, private dialog: MatDialog) { }

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
        this.loadDevices();
      },
      error: (err) => {
        alert('Falha ao adicionar dispositivo.');
        console.error(err);
      }
    });
  }

  deleteDevice(device: DeviceListResponse) {
    if (confirm(`Deseja realmente excluir o dispositivo "${device.deviceName}"?`)) {
      this.devicesService.deleteDevice(device.id).subscribe({
        next: () => {
          alert('Dispositivo excluído com sucesso!');
          this.loadDevices();
        },
        error: (err) => {
          alert('Erro ao excluir dispositivo.');
          console.error(err);
        }
      });
    }
  }

  openAddDialog() {
    const dialogRef = this.dialog.open(DeviceDialog, {
      width: '400px',
      data: { mode: 'add' } as DeviceDialogData
    });

    dialogRef.afterClosed().subscribe((result: DeviceAddRequest | undefined) => {
      if (result) {
        this.devicesService.addDevice(result).subscribe(() => {
          alert('Dispositivo adicionado com sucesso!');
          this.loadDevices();
        });
      }
    });
  }

  openEditDialog(device: DeviceListResponse) {
    const dialogRef = this.dialog.open(DeviceDialog, {
      width: '400px',
      data: { mode: 'edit', device } as DeviceDialogData
    });

    dialogRef.afterClosed().subscribe((result: DeviceAddRequest | undefined) => {
      if (result) {
        this.devicesService.updateDevice(device.id, result).subscribe({
          next: () => {
            alert('Dispositivo atualizado com sucesso!');
            this.loadDevices();
          },
          error: (err) => {
            alert('Erro ao atualizar dispositivo');
            console.error(err);
          }
        });
      }
    });
  }

  openViewDialog(d: Device) {
    this.devicesService.getDeviceById(d.id).subscribe({
      next: (res) => {
        if (res.success && res.data) {
          this.dialog.open(DeviceViewDialog, {
            width: '400px',
            data: res.data
          });
        } else {
          alert(res.errorMessages.join(', ') || 'Erro ao buscar dispositivo');
        }
      },
      error: (err) => {
        alert('Falha na comunicação com a API.');
        console.error(err);
      }
    });
  }
}
