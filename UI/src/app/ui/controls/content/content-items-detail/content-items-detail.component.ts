import { Component, OnInit, Input } from '@angular/core';
import { ContentItemInfoService } from '@app/services/content/content-item-info.service';
import { Guid } from 'guid-typescript';

@Component({
    selector: 'app-content-chapters-detail',
    templateUrl: './content-items-detail.component.html',
    styleUrls: ['./content-items-detail.component.css'],
    standalone: false
})
export class ContentItemsDetailComponent implements OnInit {

	constructor(private service: ContentItemInfoService) { }

	ngOnInit() {
		if (this.parentId && Guid.isGuid(this.parentId)) {
			this.service.getContentInfoItems(this.parentId, {
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
