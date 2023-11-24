namespace Maid.Core.Utilities
{
	using System.Net.Http;
	using System.Threading.Tasks;

	public static class ImageUtils
	{
		public static async Task<byte[]> LoadImageByUrl(string url) {
			using (HttpClient client = new HttpClient()) {
				return await client.GetByteArrayAsync(url);
			}
		}
	}
}
