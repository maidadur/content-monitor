import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-manga-sources-section',
  templateUrl: './manga-sources-section.component.html',
  styleUrls: ['./manga-sources-section.component.css']
})
export class MangaSourcesSectionComponent implements OnInit {

  constructor() { }

  items = [
    { caption: "Lucky star!" },
    { caption: "Berserk" }
  ];

  ngOnInit() {
  }

  openCard(item) {
  }

}
