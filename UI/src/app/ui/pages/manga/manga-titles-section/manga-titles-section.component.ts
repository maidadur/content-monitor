import { Component, OnInit } from '@angular/core';
import { MangaTitlesService } from '@app/services/manga/manga-titles.service';
import { MangaInfo } from '@app/entity/manga/manga-info';

@Component({
  selector: 'app-manga-titles-section',
  templateUrl: './manga-titles-section.component.html',
  styleUrls: ['./manga-titles-section.component.css']
})
export class MangaTitlesSectionComponent implements OnInit {

  items: MangaInfo[];

  constructor(private service: MangaTitlesService) { }

  ngOnInit() {
    this.service.getAll().subscribe(items => this.items = items);
  }

  openCard(item) {

  }
 
  onDeleteItem(event, item) {
    event.stopPropagation();
    this.service.delete(item.id).subscribe();
    this.items = this.items.filter(i => i.id !== item.id);
  }

}
