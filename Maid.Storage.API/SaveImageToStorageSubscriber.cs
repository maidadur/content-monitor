namespace Maid.Storage.API
{
	using Maid.Core;
	using System.Threading.Tasks;

	public class SaveImageToStorageSubscriber : IMessageConsumer
	{
		protected SaveImageToStorageTask SaveImageToStorageTask { get; }

		public SaveImageToStorageSubscriber(SaveImageToStorageTask task) {
			SaveImageToStorageTask = task;
		}

		public async Task ProcessAsync(byte[] data) {
			await SaveImageToStorageTask.SaveImageToStorage(data);
		}
	}
}
