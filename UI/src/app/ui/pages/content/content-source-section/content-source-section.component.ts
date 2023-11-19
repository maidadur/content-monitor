import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ContentSource } from '@app/entity/content/content-source';
import { ContentSourcesService } from '@app/services/content/content-source.service';
import { BaseSectionComponent } from '../../base/base-section.component';

@Component({
	selector: 'app-content-sources-section',
	templateUrl: './content-source-section.component.html',
	styleUrls: ['./content-source-section.component.css']
})
export class ContentSourceSectionComponent extends BaseSectionComponent<ContentSource> implements OnInit {

	constructor(
		public service: ContentSourcesService,
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
