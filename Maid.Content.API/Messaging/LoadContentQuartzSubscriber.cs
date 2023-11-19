namespace Maid.Content.API
{
	using Maid.Core;
	using System.Threading.Tasks;

	public class LoadContentQuartzSubscriber : IMessageConsumer
	{
		private ContentLoadTask _contentLoadTask;

		public LoadContentQuartzSubscriber(ContentLoadTask contentLoader) {
			_contentLoadTask = contentLoader;
		}

		public async Task ProcessAsync(byte[] data) {
			await _contentLoadTask.LoadContentInfoAsync();
		}
	}
}