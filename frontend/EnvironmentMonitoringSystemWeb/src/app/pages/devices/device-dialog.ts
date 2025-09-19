import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { DeviceAddRequest, DeviceListResponse } from '../../services/devices.services';
import { NgIf } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
export interface DeviceDialogData {
  mode: 'add' | 'edit' | 'view';
  device?: DeviceListResponse; 
}

@Component({
  selector: 'app-device-dialog',
  standalone: true,
  templateUrl: './device-dialog.html',
  imports: [
    MatDialogModule, 
    FormsModule, 
    NgIf,
    MatFormFieldModule,  
    MatInputModule,
    MatButtonModule,       
  ]
})
export class DeviceDialog {
  device: DeviceAddRequest = { deviceName: '', location: '' };
  mode: 'add' | 'edit' | 'view';

  constructor(
    public dialogRef: MatDialogRef<DeviceDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DeviceDialogData
  ) {
    this.mode = data.mode;
    if (data.device) {
      this.device.deviceName = data.device.deviceName;
      this.device.location = data.device.location;
    }
  }

  save() {
    this.dialogRef.close(this.device);
  }

  close() {
    this.dialogRef.close();
  }
}
