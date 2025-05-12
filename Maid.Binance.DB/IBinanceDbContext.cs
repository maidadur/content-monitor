namespace Maid.Binance.DB
{
	using Microsoft.EntityFrameworkCore;

	public interface IBinanceDbContext
	{
		DbSet<BinanceTrade> BinanceTrades { get; set; }

		DbSet<BinanceOrder> BinanceOrders { get; set; }
	}
}