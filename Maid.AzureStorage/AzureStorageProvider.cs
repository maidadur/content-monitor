using Azure.Storage.Blobs;
namespace Maid.AzureStorage
{
	using Maid.IStorage;

	public class AzureStorageProvider: IStorageProvider
	{
		protected string ConnectionString { get; }
		protected string StorageAccountName { get; }
		protected string ContainerName { get; }

		public AzureStorageProvider(string connectionString, string storageAccountName, string containerName) {
			ConnectionString = connectionString;
			StorageAccountName = storageAccountName;
			ContainerName = containerName;
		}

		private string CreateFileUrl(string fileName) {
			return $"https://{StorageAccountName}.blob.core.windows.net/{ContainerName}/{fileName}";
		}

		public async Task<string> UploadFile(byte[] fileBytes, string extension) {
			Console.Write("AzureStorageProvider UploadFile");
			BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionString);
			Console.Write("BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionString);");
			BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
			Console.Write("BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);");
			string blobName = $"{Guid.NewGuid()}.{extension}";
			using (MemoryStream memoryStream = new MemoryStream(fileBytes)) {
				await containerClient.UploadBlobAsync(blobName, memoryStream);
				Console.Write("await containerClient.UploadBlobAsync(blobName, memoryStream);");
			}
			return CreateFileUrl(blobName);
		}
	}
}