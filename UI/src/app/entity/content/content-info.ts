import { BaseEntity } from '../base-entity';

export class ContentInfo extends BaseEntity {
  name: string;
  href: string;
  imageUrl: string;
  status: string;
  isStatusPositive?: boolean;

  constructor(entity?) {
    super(entity);
  }
}
