import { BaseEntity } from '../base-entity';
import { MangaSource } from './manga-source';

export class MangaInfo extends BaseEntity {
  name: string;
  href: string;
  imageUrl: string;
  source: MangaSource;

  lookupColumns: object = {
    source: "MangaSource"
  }

  constructor(entity) {
    super(entity);
  }
}
