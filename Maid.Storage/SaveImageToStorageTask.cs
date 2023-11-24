namespace Maid.Storage
{
	using Maid.Core;
	using Maid.Core.Utilities;
	using Maid.IStorage;
	using Maid.RabbitMQ;
	using Newtonsoft.Json;

	public class SaveImageToStorageTask
	{
		private readonly IStorageProvider _storageProvider;
		private readonly IMessageClient _messageClient;

		public SaveImageToStorageTask(IStorageProvider storageProvider, IMessageClient messageClient) {
			_storageProvider = storageProvider;
			_messageClient = messageClient;
		}

		public async Task SaveImageToStorage(byte[] data) {
			Console.WriteLine("SaveImageToStorage");
			string strData = System.Text.Encoding.UTF8.GetString(data);
			Console.WriteLine("strData");
			SaveImageMessage message = JsonConvert.DeserializeObject<SaveImageMessage>(strData);
			byte[] imageBytes = await ImageUtils.LoadImageByUrl(message.ImageUrl);
			Console.WriteLine("Loaded image");
			string extention = Path.GetExtension(message.ImageUrl);
			Console.WriteLine("Image extension: " + extention);
			string newFileName = await _storageProvider.UploadFile(imageBytes, extention);
			Console.WriteLine("Uploaded image " + newFileName);
			message.ImageUrl = newFileName;
			var newData = JsonConvert.SerializeObject(message).ToBytesArray();
			_messageClient.SendMessage("save_image", newData);
		}
	}
}