import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';


@Component({
  selector: 'app-manga-page',
  templateUrl: './manga-page.component.html',
  styleUrls: ['./manga-page.component.css']
})
export class MangaPageComponent implements OnInit {
 
  ngOnInit(): void {
  }

  constructor(
    private route: ActivatedRoute,
    private location: Location
  ) { }

  goBack() {
    this.location.back();
  }
}
