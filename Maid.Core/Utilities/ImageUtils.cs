namespace Maid.Core.Utilities
{
	using System;
	using System.Net.Http;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;

	public static class ImageUtils
	{
		public static async Task<byte[]> LoadImageByUrl(string url) {
			using (HttpClient client = new HttpClient()) {
				return await client.GetByteArrayAsync(url);
			}
		}

		public static bool IsImageUrl(string input) {
			if (Uri.TryCreate(input, UriKind.Absolute, out Uri uriResult) &&
				(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)) {
				return Regex.IsMatch(uriResult.AbsolutePath, @"\.(jpg|jpeg|png|gif|bmp|svg)$", RegexOptions.IgnoreCase);
			}
			return false;
		}

		public static bool IsBase64Image(string input) {
			return Regex.IsMatch(input, @"^data:image\/(png|jpg|jpeg|gif|bmp|svg\+xml);base64,", RegexOptions.IgnoreCase);
		}

		public static string? GetImageExtensionFromBase64(string base64) {
			var match = Regex.Match(base64, @"^data:image/(?<type>[a-zA-Z0-9\+]+);base64,");
			if (match.Success) {
				return match.Groups["type"].Value switch {
					"jpeg" => ".jpg",
					"svg+xml" => ".svg",
					var ext => $".{ext}" // return as-is for png, gif, bmp, etc.
				};
			}
			return null;
		}
	}
}
