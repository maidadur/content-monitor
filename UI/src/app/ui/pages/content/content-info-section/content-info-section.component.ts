import { Component, OnInit } from '@angular/core';
import { ContentInfoService } from '@app/services/content/content-info.service';
import { ContentInfo } from '@app/entity/content/content-info';
import { BaseSectionComponent } from '../../base/base-section.component';
import { Location } from '@angular/common';

@Component({
	selector: 'app-content-info-section',
	templateUrl: './content-info-section.component.html',
	styleUrls: ['./content-info-section.component.css']
})
export class ContentInfoSectionComponent extends BaseSectionComponent<ContentInfo> implements OnInit {

	items: ContentInfo[];

	constructor(
		public service: ContentInfoService,
		location: Location
	) {
		super(service, location);
	}

	onDeleteItem(event, item) {
		event.stopPropagation();
		this.service.delete(item.id).subscribe();
		this.items = this.items.filter(i => i.id !== item.id);
	}

	public onStatusClick(item: ContentInfo) {
		event.stopPropagation();
		window.open(item.href, '_blank').focus();
	}

}
