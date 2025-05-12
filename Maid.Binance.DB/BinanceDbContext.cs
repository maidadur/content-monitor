namespace Maid.Binance.DB
{
	using Microsoft.EntityFrameworkCore;

	public class BinanceDbContext : DbContext
	{
		public BinanceDbContext(DbContextOptions<BinanceDbContext> options)
			: base(options) { }

		public DbSet<BinanceTrade> BinanceTrades { get; set; }

		public DbSet<BinanceOrder> BinanceOrders { get; set; }
		public DbSet<BinanceOrderEmotion> BinanceOrderEmotion { get; set; }
		public DbSet<BinanceOrderTag> BinanceOrderTag { get; set; }

	}
}