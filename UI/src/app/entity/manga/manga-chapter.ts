import { BaseEntity } from '../base-entity';

export class MangaChapter extends BaseEntity {
	name: string;
	date: string;
	href: string;
	mangaId: string;
}