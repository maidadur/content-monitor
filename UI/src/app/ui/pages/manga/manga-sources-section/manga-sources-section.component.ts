import { Component, OnInit } from '@angular/core';
import { MangaSourcesService } from '@app/services/manga/manga-sources.service';
import { MangaInfo } from '@app/entity/manga/manga-info';

@Component({
  selector: 'app-manga-sources-section',
  templateUrl: './manga-sources-section.component.html',
  styleUrls: ['./manga-sources-section.component.css']
})
export class MangaSourcesSectionComponent implements OnInit {

  items: MangaInfo[];

  constructor(private service: MangaSourcesService) { }

  ngOnInit() {
    this.service.getAll().subscribe(items => this.items = items);
  }

  openCard(item) {

  }

}
