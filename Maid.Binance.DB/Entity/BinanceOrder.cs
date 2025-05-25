using Maid.Core;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maid.Binance.DB
{
	public class BinanceOrder : BaseEntity
	{
		private DateTime _time;

		public long OrderId { get; set; }
		public string Symbol { get; set; }
		public string Side { get; set; }
		public decimal Quantity { get; set; }
		public decimal Pnl { get; set; }
		[NotMapped]
		public decimal CleanPnl { get; set; }
		public decimal Commission { get; set; }
		public string CommissionAsset { get; set; }
		public DateTime Time {
			get => DateTime.SpecifyKind(_time, DateTimeKind.Utc).ToUniversalTime();
			set => _time = DateTime.SpecifyKind(value, DateTimeKind.Utc).ToUniversalTime();
		}

		public string? Notes { get; set; }
		public int Leverage { get; set; }
		public string? ImageUrl { get; set; }
        public string? AISummary { get; set; }
    }
}
