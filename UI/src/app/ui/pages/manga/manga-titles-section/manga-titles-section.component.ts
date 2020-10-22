import { Component, OnInit } from '@angular/core';
import { MangaTitlesService } from '@app/services/manga/manga-titles.service';
import { MangaInfo } from '@app/entity/manga/manga-info';
import { BaseSectionComponent } from '../../base/base-section.component';
import { Location } from '@angular/common';

@Component({
	selector: 'app-manga-titles-section',
	templateUrl: './manga-titles-section.component.html',
	styleUrls: ['./manga-titles-section.component.css']
})
export class MangaTitlesSectionComponent extends BaseSectionComponent<MangaInfo> implements OnInit {

	items: MangaInfo[];

	constructor(
		public service: MangaTitlesService,
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
