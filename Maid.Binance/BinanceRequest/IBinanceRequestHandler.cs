namespace Maid.Binance
{
	public interface IBinanceRequestHandler
	{
		Task<string> GetTradeData(BinanceRequest item);
	}
}