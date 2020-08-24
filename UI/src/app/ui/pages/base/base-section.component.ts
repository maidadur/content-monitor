import { BaseEntity } from '@app/entity/base-entity';
import { BaseGenericService } from '@app/services/base-generic.service';
import { map } from 'rxjs/operators';

export class BaseSectionComponent<TEntity extends BaseEntity> {

	public items: TEntity[] = [];
	public offset = 0;
	public count = 60;

	constructor(
		public service: BaseGenericService<TEntity>
	) {}

	public ngOnInit(): void {
		this.loadData();
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
	}

	public getItemsObservable() {
		return this.service.getAll({offset: this.offset, count: this.count});
	}
}