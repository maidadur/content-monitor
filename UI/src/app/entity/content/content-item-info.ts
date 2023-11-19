import { BaseEntity } from '../base-entity';
import { ContentInfo as ContentInfo } from './content-info';

export class ContentItemInfo extends BaseEntity {
	name: string;
	date: string;
	href: string;
	contentInfoId: string;
	contentInfo: ContentInfo;
}