import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { MangaChapterNotification } from '@app/entity/manga/manga-chapter-notification';
import { AuthService } from '../auth/auth.service';
import { UrlUtils } from '@app/utils/url-utils';
import { SelectOptions } from '@app/common/select-options';

@Injectable({
	providedIn: 'root'
})
export class MangaChapterNotificationService extends BaseGenericService<MangaChapterNotification> {

	protected apiUrl = UrlUtils.replaceUrlDomain(environment.mangaHost) + '/api/new-manga';

	constructor(protected http: HttpClient, protected auth: AuthService) {
		super(http, auth);
	}

	public getAllNotifications(selectOptions: SelectOptions): Observable<MangaChapterNotification[]> {
		let url = this.apiUrl + '/updates';
		return this.http.post<MangaChapterNotification[]>(url, selectOptions, this.httpOptions).pipe(
			catchError(this.handleError)
		);
	}

	public readNotifications(notReadItems: MangaChapterNotification[]): Observable<any> {
		let url = this.apiUrl + '/read';
		var items = notReadItems.map(item => item.id);
		return this.http.post(url, items, this.httpOptions).pipe(
			catchError(this.handleError)
		);
	}
}
