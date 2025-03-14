import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ContentNotificationService } from '@app/services/content/content-notification.service';
import { ContentItemNotification } from '@app/entity/content/content-item-notification';
import { BaseSectionComponent } from '../../base/base-section.component';
import { Location } from '@angular/common';
import * as moment from 'moment';

@Component({
    selector: 'app-content-section',
    templateUrl: './content-notifications-section.component.html',
    styleUrls: ['./content-notifications-section.component.less'],
    standalone: false
})
export class ContentNotificationSectionComponent extends BaseSectionComponent<ContentItemNotification> implements OnInit {

	constructor(
			public service: ContentNotificationService,
			location: Location
		) {
		super(service, location);
	}

	private _setChaptersAsRead(items: ContentItemNotification[]) {
		const notReadItems = items.filter((item) => !item.isRead);
		if (notReadItems.length > 0) {
			this.service.readNotifications(notReadItems).subscribe();
		}
	}

	private _formatCreatedOn(items: ContentItemNotification[]) {
		items.forEach(item => {
			item.createdOn = moment.utc(item.createdOn).local().fromNow();
		});
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

	public handleGetDataResponse(items: ContentItemNotification[]) {
		super.handleGetDataResponse(items);
		this._formatCreatedOn(items);
		this._setChaptersAsRead(items);
	}
}
