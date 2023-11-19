import { Component, OnInit } from '@angular/core';
import { ContentSource } from '@app/entity/content/content-source';
import { BasePageComponent } from '../../base/base-page.component';
import { ContentSourcesService } from '@app/services/content/content-source.service';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
	selector: 'app-content-source-page',
	templateUrl: './content-source-page.component.html',
	styleUrls: ['./content-source-page.component.css']
})
export class ContentSourcePageComponent extends BasePageComponent<ContentSource> implements OnInit {
	public model: ContentSource = new ContentSource();

	constructor(public service: ContentSourcesService, router: ActivatedRoute, location: Location) {
		super(service, router, location);
	}

	public afterInsertHandler() {
		this.location.go('content/sources');
	}
}
