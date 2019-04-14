import { BaseEntity } from './base-entity';

export class BaseLookup extends BaseEntity {
  name: string;

  constructor(entity) {
    super(entity);
  }
}
