import { Component, OnInit } from '@angular/core';
import { MangaChapterService } from '@app/services/manga/manga-chapter.service';
import { MangaChapter } from '@app/entity/manga/manga-chapter';

@Component({
  selector: 'app-manga-section',
  templateUrl: './manga-section.component.html',
  styleUrls: ['./manga-section.component.css']
})
export class MangaSectionComponent implements OnInit {

  constructor(private service: MangaChapterService) { }

  items: MangaChapter[];

  ngOnInit() {
    this.service.getAll({loadLookups: true})
      .subscribe(items => this.items = items); 
  }

}
