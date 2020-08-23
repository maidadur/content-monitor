import { Component, OnInit } from '@angular/core';
import { MangaChapterNotificationService } from '@app/services/manga/manga-chapter-notification.service';
import { MangaChapterNotification } from '@app/entity/manga/manga-chapter-notification';
import { BaseSectionComponent } from '../../base/base-section.component';

@Component({
	selector: 'app-manga-section',
	templateUrl: './manga-section.component.html',
	styleUrls: ['./manga-section.component.css']
})
export class MangaSectionComponent extends BaseSectionComponent<MangaChapterNotification> implements OnInit {

	constructor(public service: MangaChapterNotificationService) {
		super(service);
	}

	public getItemsObservable() {
		return this.service.getAllNotifications({offset: this.offset, count: this.count});
	}
}
