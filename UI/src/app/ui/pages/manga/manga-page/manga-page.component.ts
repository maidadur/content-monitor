import { Component, OnInit, OnChanges, SimpleChanges, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { MangaTitlesService } from '@app/services/manga/manga-titles.service';
import { MangaInfo } from '@app/entity/manga/manga-info';
import { CardMode } from '@app/entity/card-mode';
import { Guid } from 'guid-typescript';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog} from '@angular/material';
import { DialogData } from '@app/ui/controls/dialog-window/dialog-data';
import { DialogWindowComponent } from '@app/ui/controls/dialog-window/dialog-window.component';
import { BasePageComponent } from '../../base/base-page-component';

@Component({
	selector: 'app-manga-page',
	templateUrl: './manga-page.component.html',
	styleUrls: ['./manga-page.component.css'],
	providers: []
})
export class MangaPageComponent extends BasePageComponent<MangaInfo> implements OnInit {

	public model: MangaInfo = new MangaInfo();
  
	constructor(
			public service: MangaTitlesService, 
			public router: ActivatedRoute, 
			public location: Location,
			public dialog: MatDialog
		) {
		super(service, router, location);
	 }

	openDialog(callback, scope): void {
		const dialogRef = this.dialog.open(DialogWindowComponent, {
		  width: '250px',
		  data: {caption: "Would you like to load manga data from the source?"}
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
				this.service.loadMangaInfo(this.model.id).subscribe(() => {
					this.loadData();
				});
			})
		}, this);
	}
}
