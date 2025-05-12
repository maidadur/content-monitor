namespace Maid.Storage
{
	using Maid.Core;
	using Maid.Core.Utilities;
	using Maid.IStorage;
	using Maid.RabbitMQ;
	using Newtonsoft.Json;
	using System.Text;
	using System.Text.RegularExpressions;

	public class SaveImageToStorageTask
	{
		private readonly IStorageProvider _storageProvider;
		private readonly IMessageClient _messageClient;

		public SaveImageToStorageTask(IStorageProvider storageProvider, IMessageClient messageClient) {
			_storageProvider = storageProvider;
			_messageClient = messageClient;
		}

		public async Task SaveImageToStorage(byte[] data) {
			string strData = Encoding.UTF8.GetString(data);
			SaveImageMessage message = JsonConvert.DeserializeObject<SaveImageMessage>(strData);
			byte[] imageBytes = null;
			string extention = null;
			if (ImageUtils.IsImageUrl(message.ImageUrl)) {
				imageBytes = await ImageUtils.LoadImageByUrl(message.ImageUrl);
				extention = Path.GetExtension(message.ImageUrl);
			} else if (ImageUtils.IsBase64Image(message.ImageUrl)) {
				Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
				var imageUrl = regex.Replace(message.ImageUrl, string.Empty);
				imageBytes = Convert.FromBase64String(imageUrl);
				extention = ImageUtils.GetImageExtensionFromBase64(message.ImageUrl);
			}
			string newFileName = await _storageProvider.UploadFile(imageBytes, extention, message.ContainerName);
			Console.WriteLine("Uploaded image " + newFileName);
			message.ImageUrl = newFileName;
			_messageClient.SendMessage(message.CallbackQueueName, message);
		}
	}
}