import { BaseLookup } from '../base-lookup';

export class MangaSource extends BaseLookup {

	domainUrl: string;
	imageUrl: string;
	
	titleXpath: string;
	imageXpath: string;

	chapterXpath: string;
	chapterTitleXpath: string;
	chapterDateXpath: string;
	chapterHrefXpath: string;

	constructor(entity?) {
		super(entity);
	}
}
