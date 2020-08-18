import { BaseEntity } from '../base-entity';

export class MangaChapterNotification extends BaseEntity {
	public name: string
	public date: string;
	public href: string;
	public mangaName: string;
	public imageUrl: string;
}