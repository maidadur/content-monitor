import { BaseEntity } from '../base-entity';

export class MangaInfo extends BaseEntity {
  name: string;
  href: string;
  imageUrl: string;

  constructor(entity?) {
    super(entity);
  }
}
