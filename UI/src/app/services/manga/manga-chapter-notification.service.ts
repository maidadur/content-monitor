import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { MangaChapter } from '@app/entity/manga/manga-chapter';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { MangaChapterNotification } from '@app/entity/manga/manga-chapter-notification';
import { AuthService } from '../auth/auth.service';

@Injectable({
	providedIn: 'root'
})
export class MangaChapterNotificationService extends BaseGenericService<MangaChapterNotification> {

	protected apiUrl = environment.mangaUrl + '/new-manga';

	constructor(protected http: HttpClient, protected auth: AuthService) {
		super(http, auth);
	}

	public getAllNotifications(params?: any): Observable<MangaChapterNotification[]> {
		let url = this.apiUrl + '/updates';
		return this.http.get<MangaChapterNotification[]>(url, Object.assign(this.httpOptions, {
			params: params
		})).pipe(
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
