import { Component, OnInit } from '@angular/core';
import { MangaSource } from '@app/entity/manga/manga-source';
import { MangaSourcesService } from '@app/services/manga/manga-sources.service';

@Component({
  selector: 'app-manga-sources-section',
  templateUrl: './manga-sources-section.component.html',
  styleUrls: ['./manga-sources-section.component.css']
})
export class MangaSourcesSectionComponent implements OnInit {

	items: MangaSource[];

	constructor(private service: MangaSourcesService) { }
  
	ngOnInit() {
	  this.service.getAll({}).subscribe(items => this.items = items);
	}
   
	onDeleteItem(event, item) {
	  event.stopPropagation();
	  this.service.delete(item.id).subscribe();
	  this.items = this.items.filter(i => i.id !== item.id);
	}

}
