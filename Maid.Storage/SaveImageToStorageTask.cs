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

		public SaveImageToStorageTask(IStorageProvider storageProvider, IMessageClient messageClient)
        {
			_storageProvider = storageProvider;
			_messageClient = messageClient;
		}

        public async Task SaveImageToStorage(byte[] data) {
			string strData = System.Text.Encoding.UTF8.GetString(data);
			SaveImageMessage message = JsonConvert.DeserializeObject<SaveImageMessage>(strData);
			byte[] imageBytes = await ImageUtils.LoadImageByUrl(message.ImageUrl);
			string extention = Path.GetExtension(message.ImageUrl);
			string newFileName = await _storageProvider.UploadFile(imageBytes, extention);
			message.ImageUrl = newFileName;
			var newData = JsonConvert.SerializeObject(message).ToBytesArray();
			_messageClient.SendMessage("save_image", newData);
		}
	}
}