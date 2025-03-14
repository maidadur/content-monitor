import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'app-base-detail',
    templateUrl: './base-detail.component.html',
    styleUrls: ['./base-detail.component.css'],
    standalone: false
})
export class BaseDetailComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  @Input()
  schemaName: string;

  @Input()
  parentId: string;

  columns: any[];

  rows: any[];
}
