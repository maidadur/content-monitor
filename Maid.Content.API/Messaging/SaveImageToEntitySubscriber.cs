using Maid.Content.Content;
using Maid.Core;
using System.Threading.Tasks;

namespace Maid.Content.API.Messaging
{
	public class SaveImageToEntitySubscriber : IMessageConsumer
	{
		private SaveImageToEntityTask _saveImageToEntityTask;

		public SaveImageToEntitySubscriber(SaveImageToEntityTask task) {
			_saveImageToEntityTask = task;

		}
		public async Task ProcessAsync(byte[] data) {
			await _saveImageToEntityTask.SaveImageToEntity(data);
		}
	}
}
