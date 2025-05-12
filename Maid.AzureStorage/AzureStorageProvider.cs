using Azure.Storage.Blobs;
namespace Maid.AzureStorage
{
	using Maid.IStorage;

	public class AzureStorageProvider: IStorageProvider
	{
		protected string ConnectionString { get; }
		protected string StorageAccountName { get; }

		public AzureStorageProvider(string connectionString, string storageAccountName) {
			ConnectionString = connectionString;
			StorageAccountName = storageAccountName;
		}

		private string CreateFileUrl(string fileName, string containerName) {
			return $"https://{StorageAccountName}.blob.core.windows.net/{containerName}/{fileName}";
		}

		public async Task<string> UploadFile(byte[] fileBytes, string extension, string containerName) {
			BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionString);
			BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
			string blobName = $"{Guid.NewGuid()}{extension}";
			using (MemoryStream memoryStream = new MemoryStream(fileBytes)) {
				await containerClient.UploadBlobAsync(blobName, memoryStream);
			}
			return CreateFileUrl(blobName, containerName);
		}
	}
}