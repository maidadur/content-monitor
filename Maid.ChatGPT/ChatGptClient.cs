namespace Maid.ChatGPT
{
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Text;
	using System.Text.Json;

	public class ChatGptClient
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		public ChatGptClient(string apiKey) {
			_apiKey = apiKey;
			_httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
		}

		public async Task<string> SendTextAndImageAsync(string prompt, string imageUrlOrBase64) {
			var requestBody = new {
				model = "gpt-4o",
				messages = new[]
				{
				new
				{
					role = "user",
					content = new object[]
					{
						new { type = "text", text = prompt },
						new { type = "image_url", image_url = new { url = imageUrlOrBase64 } }
					}
				}
			},
				max_tokens = 1500
			};

			var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

			if (!response.IsSuccessStatusCode) {
				var error = await response.Content.ReadAsStringAsync();
				throw new Exception($"API Error: {response.StatusCode} — {error}");
			}

			var responseJson = await response.Content.ReadAsStringAsync();
			using var doc = JsonDocument.Parse(responseJson);
			var reply = doc.RootElement
				.GetProperty("choices")[0]
				.GetProperty("message")
				.GetProperty("content")
				.GetString();

			return reply;
		}

		public static string EncodeImageToBase64(string path) {
			var bytes = File.ReadAllBytes(path);
			var base64 = Convert.ToBase64String(bytes);
			return $"data:image/{Path.GetExtension(path).TrimStart('.')};base64,{base64}";
		}
	}
}
