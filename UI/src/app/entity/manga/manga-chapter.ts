import { BaseEntity } from '../base-entity';
import { MangaInfo } from './manga-info';

export class MangaChapter extends BaseEntity {
	name: string;
	date: string;
	href: string;
	mangaId: string;
	manga: MangaInfo;
}