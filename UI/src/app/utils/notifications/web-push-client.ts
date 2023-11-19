import { Injectable } from '@angular/core';
import { SwPush } from '@angular/service-worker'
import { PushNotificationsService } from '@app/services/notifications/push-notifications-service';
import { Observable } from 'rxjs';
import { mergeMap } from 'rxjs/operators';

@Injectable({
	providedIn: 'root',
})
export class WebPushClient {
	
	private _subscriptionInstance: PushSubscription;

	constructor(
			private _swPush: SwPush,
			private _notificationsService: PushNotificationsService
		) {
	}

	public init(): Observable<void> {
		return new Observable((observer) => {
			this._swPush.subscription.subscribe((subscription) =>  {
				this._subscriptionInstance = subscription;
				observer.next();
			});
		});
	}

	public subscribeEndpoint() {
		if (this._subscriptionInstance) {
			return;
		}
		this._notificationsService.getKey()
			.pipe(
				mergeMap((publicKey) => {
					return this._swPush.requestSubscription({
						serverPublicKey: publicKey,
					
					  });
				}),
				mergeMap((subscription) => {
					return this._notificationsService.saveSubscription(subscription);
				})
			)
		.subscribe();
	}
}