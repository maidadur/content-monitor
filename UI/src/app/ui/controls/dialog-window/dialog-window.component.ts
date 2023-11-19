import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef as MatDialogRef, MAT_DIALOG_DATA as MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogData } from './dialog-data';
import { MatDialogModule } from '@angular/material/dialog';

@Component({
  selector: 'app-dialog-window',
  templateUrl: './dialog-window.component.html',
  styleUrls: ['./dialog-window.component.css']
})
export class DialogWindowComponent {
  model: DialogData;

  constructor(
    public dialogRef: MatDialogRef<DialogWindowComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {
    this.model = data;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
