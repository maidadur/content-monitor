import { BaseEntity } from "../base-entity";

export class BinanceOrderTag extends BaseEntity {
	binanceOrderId: string;
	tag: string;
}