import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common'; 
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-device-view-dialog',
  standalone: true,
  templateUrl: './device-view-dialog.html',
  imports: [
    MatDialogModule,
    CommonModule,
    MatInputModule,
    MatButtonModule, 
  ]
})
export class DeviceViewDialog {
  constructor(
    public dialogRef: MatDialogRef<DeviceViewDialog>,
    @Inject(MAT_DIALOG_DATA) public device: any
  ) {}
}
