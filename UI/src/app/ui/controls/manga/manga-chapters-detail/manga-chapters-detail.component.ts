import { Component, OnInit, Input } from '@angular/core';
import { MangaChapterService } from '@app/services/manga/manga-chapter.service';
import { Guid } from 'guid-typescript';

@Component({
	selector: 'app-manga-chapters-detail',
	templateUrl: './manga-chapters-detail.component.html',
	styleUrls: ['./manga-chapters-detail.component.css']
})
export class MangaChaptersDetailComponent implements OnInit {

	constructor(private service: MangaChapterService) { }

	ngOnInit() {
		if (this.parentId && Guid.isGuid(this.parentId)) {
			this.service.getMangaChapters(this.parentId, {
				orderOptions: [
					{
						column: "CreatedOn",
						isAscending: false
					}
				]
			}).subscribe(items => this.rows = items);
		}
	}

	@Input()
	parentId: string;

	columns: any[];

	rows: any[];

}
