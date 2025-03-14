import { Component, OnInit } from '@angular/core';
import { WebPushClient } from '@app/utils/notifications/web-push-client';

@Component({
    selector: 'app-workspace',
    templateUrl: './workspace.component.html',
    styleUrls: ['./workspace.component.css'],
    standalone: false
})
export class WorkspaceComponent implements OnInit {

	constructor(private _webPushClient: WebPushClient) {
	}

	ngOnInit(): void {
		this._webPushClient.init().subscribe(() => {
			this._webPushClient.subscribeEndpoint();
		});
	}

}
