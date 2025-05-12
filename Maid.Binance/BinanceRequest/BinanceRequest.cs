using Maid.Core;

namespace Maid.Binance
{
	public class BinanceRequest
	{
		public string Url { get; set; }
		public IEnumerable<QueryParam>? Params { get; set; }

	}
}