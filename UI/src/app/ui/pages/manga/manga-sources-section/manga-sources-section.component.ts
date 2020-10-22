import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MangaSource } from '@app/entity/manga/manga-source';
import { MangaSourcesService } from '@app/services/manga/manga-sources.service';
import { BaseSectionComponent } from '../../base/base-section.component';

@Component({
	selector: 'app-manga-sources-section',
	templateUrl: './manga-sources-section.component.html',
	styleUrls: ['./manga-sources-section.component.css']
})
export class MangaSourcesSectionComponent extends BaseSectionComponent<MangaSource> implements OnInit {

	constructor(
		public service: MangaSourcesService,
		location: Location
	) {
		super(service, location);
	 }


	onDeleteItem(event, item) {
		event.stopPropagation();
		this.service.delete(item.id).subscribe();
		this.items = this.items.filter(i => i.id !== item.id);
	}

}
