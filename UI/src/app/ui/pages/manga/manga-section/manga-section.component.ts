import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MangaChapterNotificationService } from '@app/services/manga/manga-chapter-notification.service';
import { MangaChapterNotification } from '@app/entity/manga/manga-chapter-notification';
import { BaseSectionComponent } from '../../base/base-section.component';
import { Location } from '@angular/common';

@Component({
	selector: 'app-manga-section',
	templateUrl: './manga-section.component.html',
	styleUrls: ['./manga-section.component.less']
})
export class MangaSectionComponent extends BaseSectionComponent<MangaChapterNotification> implements OnInit {

	constructor(
			public service: MangaChapterNotificationService,
			location: Location
		) {
		super(service, location);
	}

	public getItemsObservable() {
		return this.service.getAllNotifications({ 
			offset: this.offset, 
			count: this.count,
			orderOptions: [{
					column: "CreatedOn",
					isAscending: false
				}
			]
		});
	}

	public handleGetDataResponse(items: MangaChapterNotification[]) {
		super.handleGetDataResponse(items);
		const notReadItems = items.filter((item) => !item.isRead);
		if (notReadItems.length > 0) {
			this.service.readNotifications(notReadItems).subscribe();
		}
	}
}
