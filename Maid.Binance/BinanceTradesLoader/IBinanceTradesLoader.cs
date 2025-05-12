using Maid.Binance.DB;

namespace Maid.Binance
{
	public interface IBinanceTradesLoader
	{
		Task<IEnumerable<BinanceTrade>> LoadNewTrades();
	}
}