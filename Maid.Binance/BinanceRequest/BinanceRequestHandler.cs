using Microsoft.Extensions.Configuration;

namespace Maid.Binance
{
	public class BinanceRequestHandler : IBinanceRequestHandler
	{
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;

		public BinanceRequestHandler(IConfiguration configuration) {
			_httpClient = new HttpClient();
			_configuration = configuration;
		}

		public async Task<string> GetTradeData(BinanceRequest item) {
			var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
			var query = $"timestamp={timestamp}";
			var queryParams = item.Params?.Select(p => $"{p.Name}={p.Value}");
			if (queryParams != null) {
				var queryParamsStr = string.Join("&", queryParams);
				query += "&" + queryParamsStr;
			}

			// Generate signature
			var signature = BinanceSigner.Sign(query, _configuration["Binance:SecretKey"]);
			var fullQuery = $"{query}&signature={signature}";

			var request = new HttpRequestMessage(
				HttpMethod.Get,
				$"{item.Url}?{fullQuery}"
			);

			request.Headers.Add("X-MBX-APIKEY", _configuration["Binance:ApiKey"]);

			var response = await _httpClient.SendAsync(request);
			var content = await response.Content.ReadAsStringAsync();
			return content;
		}
	}
}
