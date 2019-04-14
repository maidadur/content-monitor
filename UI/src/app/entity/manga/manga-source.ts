import { BaseEntity } from '../base-entity';
import { BaseLookup } from '../base-lookup';

export class MangaSource extends BaseLookup {
  domainUrl: string;

  constructor(entity) {
    super(entity);
  }
}
