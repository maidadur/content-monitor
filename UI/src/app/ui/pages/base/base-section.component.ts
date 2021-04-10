import { BaseEntity } from '@app/entity/base-entity';
import { BaseGenericService } from '@app/services/base-generic.service';
import { map } from 'rxjs/operators';
import { Component, ViewChild } from '@angular/core';
import { Location } from '@angular/common';
import { ViewPortComponent } from '@app/ui/controls/view-port/view-port.component';

@Component({template: ''})
export class BaseSectionComponent<TEntity extends BaseEntity> {

	public items: TEntity[] = [];
	public parentBody: any;

	public offset = 0;
	public count = 60;

	@ViewChild(ViewPortComponent)
	private _viewPortEl: ViewPortComponent;

	constructor(
		public service: BaseGenericService<TEntity>,
		public location: Location
	) {
		this.parentBody = document;
	}

	public ngOnInit(): void {
		this.loadData();
	}

	public goBack() {
		this.location.back();
	}

	public loadData(): void {
		this.getItemsObservable()
			.pipe(
				map((items) => this.handleGetDataResponse(items))
			)
			.subscribe();
	}

	public handleGetDataResponse(items: TEntity[]) {
		this.items = this.items.concat(items);
		this.offset += items.length;
		if (items.length < this.count) {
			this._viewPortEl.stopPropagation = true;
		} else {
			this._viewPortEl.stopPropagation = false;
		}
	}

	public getItemsObservable() {
		return this.service.getAll({offset: this.offset, count: this.count});
	}

	public onViewPortVisible() {
		this._viewPortEl.stopPropagation = true;
		this.loadData();
	}
}