namespace Maid.IStorage
{
	public interface IStorageProvider
	{
		Task<string> UploadFile(byte[] fileBytes, string extension);
	}
}