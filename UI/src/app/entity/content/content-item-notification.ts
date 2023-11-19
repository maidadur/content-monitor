import { BaseEntity } from '../base-entity';

export class ContentItemNotification extends BaseEntity {
	public name: string
	public date: string;
	public href: string;
	public contentName: string;
	public imageUrl: string;
	public isRead: boolean;
}