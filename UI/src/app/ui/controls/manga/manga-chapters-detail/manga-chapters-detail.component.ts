import { Component, OnInit, Input } from '@angular/core';
import { MangaChapterService } from '@app/services/manga/manga-chapter.service';

@Component({
  selector: 'app-manga-chapters-detail',
  templateUrl: './manga-chapters-detail.component.html',
  styleUrls: ['./manga-chapters-detail.component.css']
})
export class MangaChaptersDetailComponent implements OnInit {

  constructor(private service: MangaChapterService) { }

  ngOnInit() {
    this.service.getMangaChapters(this.parentId)
      .subscribe(items => this.rows = items);
  }

  @Input()
  parentId: string;

  columns: any[];

  rows: any[];

}
