using Maid.Core;

namespace Maid.Binance
{
	public class LoadBinanceTradesQuartzSubscriber : IMessageConsumer
	{
		private IBinanceLoadDataTask _loadDataTask;

		public LoadBinanceTradesQuartzSubscriber(IBinanceLoadDataTask loadDataTask) {
			_loadDataTask = loadDataTask;
		}
		public async Task ProcessAsync(byte[] data) {
			await _loadDataTask.LoadData();
		}
	}
}
