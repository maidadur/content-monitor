import { BaseEntity } from '../base-entity';

export class BinanceOrder extends BaseEntity {
  imageUrl: string;
  orderId: string;
  symbol: string;
  side: string;
  quantity: number;
  pnl: number;
  cleanPnl: number;
  commission: number;
  commissionAsset: string;
  time: string;
  notes: string;
  leverage: number;
  aiSummary: string;

  constructor(entity?) {
	super(entity);
  }
}
