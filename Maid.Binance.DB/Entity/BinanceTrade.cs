using Maid.Core;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Maid.Binance.DB
{
	public class BinanceTrade : BaseEntity
	{
		[JsonProperty("id")]
		public long TradeId { get; set; }
		public string Symbol { get; set; }
		public string Side { get; set; }
		public long OrderId { get; set; }
		[JsonProperty("realizedPnl")]
		public decimal Pnl { get; set; }
		[JsonProperty("qty")]
		public decimal Quantity { get; set; }
		public decimal Commission { get; set; }
		public string CommissionAsset { get; set; }
		public long Time { get; set; }
	}
}