import { Component, OnInit } from '@angular/core';
import { MangaSource } from '@app/entity/manga/manga-source';
import { BasePageComponent } from '../../base/base-page.component';
import { MangaSourcesService } from '@app/services/manga/manga-sources.service';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
	selector: 'app-manga-source-page',
	templateUrl: './manga-source-page.component.html',
	styleUrls: ['./manga-source-page.component.css']
})
export class MangaSourcePageComponent extends BasePageComponent<MangaSource> implements OnInit {
	public model: MangaSource = new MangaSource();

	constructor(public service: MangaSourcesService, router: ActivatedRoute, location: Location) {
		super(service, router, location);
	}

	public afterInsertHandler() {
		this.location.go('manga/sources');
	}
}
