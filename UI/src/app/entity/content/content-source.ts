import { BaseLookup } from '../base-lookup';

export class ContentSource extends BaseLookup {

	domainUrl: string;
	imageUrl: string;
	
	titleXpath: string;
	imageXpath: string;

	collectionItemXpath?: string;
	collectionItemTitleXpath?: string;
	collectionItemDateXpath?: string;
	collectionItemHrefXpath?: string;

	statusXpath?: string;
	positiveStatusText?: string;

	constructor(entity?) {
		super(entity);
	}
}
