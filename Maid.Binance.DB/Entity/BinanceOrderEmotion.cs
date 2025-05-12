using Maid.Core;

namespace Maid.Binance.DB
{
	public class BinanceOrderEmotion : BaseEntity
	{
		public Guid BinanceOrderId { get; set; }
		public BinanceOrder? BinanceOrder { get; set; }

		public string Emotion { get; set; }
	}
}
