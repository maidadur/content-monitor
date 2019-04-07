import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-manga-section',
  templateUrl: './manga-section.component.html',
  styleUrls: ['./manga-section.component.css']
})
export class MangaSectionComponent implements OnInit {

  constructor() { }

  tiles = [
    { text: 'One', cols: 1, rows: 1, color: '#142A5C' },
    { text: 'Two', cols: 1, rows: 1, color: '#B7A0E8' },
    { text: 'Three', cols: 1, rows: 1, color: '#FF0000' },
    { text: 'Four', cols: 1, rows: 1, color: '#D9EDD9' },
  ];

  ngOnInit() {
  }

}
