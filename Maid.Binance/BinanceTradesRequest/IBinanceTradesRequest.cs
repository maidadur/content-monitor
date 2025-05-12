using Maid.Binance.DB;

namespace Maid.Binance
{
    public interface IBinanceTradesRequest
	{
		Task<IEnumerable<BinanceTrade>> RequestTrades(LoadTradesRequest request);
	}
}