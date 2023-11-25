export class SelectOptions {
	loadLookups?: boolean;
	offset?: number;
	count?: number;
	orderOptions?: [{
		column: string,
		isAscending?: boolean
	}]
}