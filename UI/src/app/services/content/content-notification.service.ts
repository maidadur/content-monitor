import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { ContentItemNotification } from '@app/entity/content/content-item-notification';
import { AuthService } from '../auth/auth.service';
import { UrlUtils } from '@app/utils/url-utils';
import { SelectOptions } from '@app/common/select-options';

@Injectable({
	providedIn: 'root'
})
export class ContentNotificationService extends BaseGenericService<ContentItemNotification> {

	protected apiUrl = UrlUtils.replaceUrlDomain(environment.contentHost) + '/api/new-content';

	constructor(protected http: HttpClient, protected auth: AuthService) {
		super(http, auth);
	}

	public getAllNotifications(selectOptions: SelectOptions): Observable<ContentItemNotification[]> {
		let url = this.apiUrl + '/updates';
		return this.http.post<ContentItemNotification[]>(url, selectOptions, this.httpOptions).pipe(
			catchError(this.handleError)
		);
	}

	public readNotifications(notReadItems: ContentItemNotification[]): Observable<any> {
		let url = this.apiUrl + '/read';
		var items = notReadItems.map(item => item.id);
		return this.http.post(url, items, this.httpOptions).pipe(
			catchError(this.handleError)
		);
	}
}
