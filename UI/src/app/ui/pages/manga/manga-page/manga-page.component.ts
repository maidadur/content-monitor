import { Component, OnInit, OnChanges, SimpleChanges, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { MangaSourcesService } from '@app/services/manga/manga-sources.service';
import { MangaInfo } from '@app/entity/manga/manga-info';
import { LookupService } from '@app/services/lookup.service';
import { CardMode } from '@app/entity/card-mode';
import { Guid } from 'guid-typescript';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog} from '@angular/material';
import { DialogData } from '@app/ui/controls/dialog-window/dialog-data';
import { DialogWindowComponent } from '@app/ui/controls/dialog-window/dialog-window.component';

@Component({
	selector: 'app-manga-page',
	templateUrl: './manga-page.component.html',
	styleUrls: ['./manga-page.component.css'],
	providers: []
})
export class MangaPageComponent implements OnInit, OnChanges {

	model: MangaInfo = new MangaInfo({});
	isChanged: boolean;
	cardMode: CardMode;

	sourceValues = [];
	sourceId: string;

	constructor(
		private route: ActivatedRoute,
		private service: MangaSourcesService,
		private lookupService: LookupService,
		private location: Location,
		private router: Router,
		public dialog: MatDialog
	) { }

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

	setupLookupValuesForSave(): void {
		for (let columnName in this.model.lookupColumns) {
			let lookupValuesColumn = columnName + "Values";
			if (!this[lookupValuesColumn]) {
				this[lookupValuesColumn] = [];
			}
			var lookupValue = this[columnName + "Id"];
			var item = this[lookupValuesColumn].find(item => item.id == lookupValue);
			this.model[columnName] = item || null;
		}
	}

	ngOnInit(): void {
		this.isChanged = false;
		let id = this.route.snapshot.paramMap.get('id');
		if (Guid.isGuid(id)) {
			this.model.id = Guid.parse(id).toString();
		}
		this.loadData();
	}

	protected loadData(): void {
		let id = this.model.id;
		if (id && Guid.isGuid(id)) {
			this.service.get(id)
			.subscribe(this.onAfterItemLoad.bind(this));
		} else {
			this.cardMode = CardMode.New;
			this.loadLookupValues();
		}
	}

	onAfterItemLoad(item) {
		if (item) {
			this.cardMode = CardMode.Edit;
		} else {
			this.cardMode = CardMode.New;
		}
		this.model = new MangaInfo(item);
		this.loadLookupValues();
	}

	loadLookupValues() {
		for (let columnName in this.model.lookupColumns) {
			let lookupSchema = this.model.lookupColumns[columnName];
			let lookupValuesColumn = columnName + "Values";
			if (!this[lookupValuesColumn]) {
				this[lookupValuesColumn] = [];
			}
			this[columnName + "Id"] = this.model[columnName] && this.model[columnName].id;
			this.fillLookupValues(lookupSchema, lookupValuesColumn);
		}
	}

	fillLookupValues(lookupSchema: string, lookupValuesColumn: string) {
		this.lookupService.schemaName = lookupSchema;
		this.lookupService.getAll().subscribe(items => {
			this[lookupValuesColumn] = items
		});
	}

	ngOnChanges(changes: SimpleChanges) {
		this.isChanged = true;
	}

	onHrefPaste(event: any) {
		let clipboardData = event.clipboardData;
		let url = clipboardData.getData('text');
		this.openDialog(function () {
			this.model.href = url;
			this.save(function () {
				this.service.loadMangaInfo(this.model.id).subscribe(() => {
					this.loadData();
				});
			}.bind(this));
		}, this);
	}

	goBack() {
		this.location.back();
	}

	updateEntity(callback: Function) {
		this.service.update(this.model).subscribe(() => {
			callback();
		});
	}

	insertEntity(callback: Function) {
		this.model.id = Guid.create().toString();
		this.service.add(this.model).subscribe(() => {
			this.cardMode = CardMode.Edit;
			callback()
		});
	}

	save(callback: Function) {
		callback = callback || function () { };
		this.setupLookupValuesForSave();
		if (this.cardMode == CardMode.New) {
			this.insertEntity(callback);
		} else {
			this.updateEntity(callback)
		}
	}
}
