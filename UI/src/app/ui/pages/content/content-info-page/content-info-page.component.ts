import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { ContentInfoService } from '@app/services/content/content-info.service';
import { ContentInfo } from '@app/entity/content/content-info';
import { MatDialog } from '@angular/material/dialog';
import { DialogWindowComponent } from '@app/ui/controls/dialog-window/dialog-window.component';
import { BasePageComponent } from '../../base/base-page.component';

@Component({
    selector: 'app-content-info-page',
    templateUrl: './content-info-page.component.html',
    styleUrls: ['./content-info-page.component.css'],
    providers: [],
    standalone: false
})
export class ContentInfoPageComponent extends BasePageComponent<ContentInfo> implements OnInit {

	public model: ContentInfo = new ContentInfo();
  
	constructor(
			public service: ContentInfoService, 
			public router: ActivatedRoute, 
			public location: Location,
			public dialog: MatDialog
		) {
		super(service, router, location);
	 }

	openDialog(callback, scope): void {
		const dialogRef = this.dialog.open(DialogWindowComponent, {
		  width: '250px',
		  data: {caption: "Would you like to load content data from the source?"}
		});
		dialogRef.afterClosed().subscribe(result => {
			if (result) {
				callback.apply(scope);
			}
		});
	  }

	onHrefPaste(event: any) {
		let clipboardData = event.clipboardData;
		let url = clipboardData.getData('text');
		this.openDialog(function () {
			this.model.href = url;
			this.save().subscribe(() => {
				this.service.loadContentInfo(this.model).subscribe(() => {
					this.loadData();
				});
			})
		}, this);
	}
}
