import { BaseEntity } from "../base-entity";

export class BinanceOrderEmotion extends BaseEntity {
	binanceOrderId: string;
	emotion: string;
}