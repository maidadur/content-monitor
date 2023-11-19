import { BaseEntity } from '../base-entity';

export class ContentInfo extends BaseEntity {
  name: string;
  href: string;
  imageUrl: string;
  status: string;

  constructor(entity?) {
    super(entity);
  }
}
