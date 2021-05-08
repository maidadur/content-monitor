import { BaseEntity } from '@app/entity/base-entity';
import { BaseGenericService } from '@app/services/base-generic.service';
import { Guid } from 'guid-typescript';
import { CardMode } from '@app/entity/card-mode';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { tap } from 'rxjs/operators';
import { Component } from '@angular/core';

@Component({template: ''})
export class BasePageComponent<TEntity extends BaseEntity> {

	public cardMode: CardMode;
	public isChanged: boolean;
	public model: TEntity;

	constructor(
		public service: BaseGenericService<TEntity>,
		public route: ActivatedRoute,
		public location: Location,
	) {}

	public ngOnInit(): void {
		this.isChanged = false;
		let id = this.route.snapshot.paramMap.get('id');
		if (Guid.isGuid(id)) {
			this.model.id = Guid.parse(id).toString();
		}
		this.loadData();
	}

	public loadData(): void {
		let id = this.model.id;
		if (id && Guid.isGuid(id)) {
			this.service.get(id)
				.subscribe((item) => {
					this.onAfterItemLoad(item);
				});
		} else {
			this.cardMode = CardMode.New;
		}
	}

	public onAfterItemLoad(item: TEntity) {
		if (item) {
			this.cardMode = CardMode.Edit;
		} else {
			this.cardMode = CardMode.New;
		}
		this.model = item;
	}

	public goBack() {
		this.location.back();
	}

	public afterUpdateHandler() {}

	public afterInsertHandler(){}

	public updateEntity() {
		return this.service.update(this.model)
			.pipe(tap(() => {
				this.afterUpdateHandler();
			}));
	}

	public insertEntity() {
		this.model.id = Guid.create().toString();
		return this.service.add(this.model)
			.pipe(tap(() => {
				this.cardMode = CardMode.Edit;
				this.afterInsertHandler();
			}));
	}

	public save() {
		if (this.cardMode == CardMode.New) {
			return this.insertEntity();
		} else {
			return this.updateEntity()
		}
	}

	public doSave() {
		this.save().subscribe();
	}

}