using Maid.Core;

namespace Maid.Binance
{
	public class GenerateOrderAISummarySubscriber : IMessageConsumer
	{
		private IGenerateOrderAISummaryTask _task;

		public GenerateOrderAISummarySubscriber(IGenerateOrderAISummaryTask task) {
			_task = task;
		}
		public async Task ProcessAsync(byte[] data) {
			await _task.GenerateSummary();
		}
	}
}
