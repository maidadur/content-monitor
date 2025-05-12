namespace Maid.Binance
{
	public class LoadTradesRequest
	{
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public int Limit { get; set; } = 1000;
    }
}