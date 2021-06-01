import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class NotificationsService {

	protected apiUrl = environment.notificationsHost + '/api';

	constructor(
			private _http: HttpClient,
		) {
	}

	public getKey(): Observable<string> {
		return this._http.get(this.apiUrl + '/key', { responseType: 'text' });
	}

	public saveSubscription(subscription) {
		return this._http.post(this.apiUrl + '/subscriptions', subscription);
	}
}