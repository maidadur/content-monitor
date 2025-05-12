using Maid.Binance.DB;
using Maid.Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Maid.Binance
{
    public class BinanceTradesRequest : IBinanceTradesRequest
	{
		private IBinanceRequestHandler _requestHandler;
		private readonly IConfiguration _configuration;

		public BinanceTradesRequest(IBinanceRequestHandler requestHandler, IConfiguration configuration) {
			_requestHandler = requestHandler;
			_configuration = configuration;
		}

		public async Task<IEnumerable<BinanceTrade>> RequestTrades(LoadTradesRequest request) {
			var parameters = new List<QueryParam> {
				new QueryParam { Name = "limit", Value = request.Limit.ToString() }
			};
			if (request.StartDate.HasValue) {
				parameters.Add(new QueryParam() { Name = "startTime", Value = ((DateTimeOffset)request.StartDate.Value).ToUnixTimeMilliseconds().ToString() });
			}
			if (request.EndDate.HasValue) {
				parameters.Add(new QueryParam() { Name = "endTime", Value = ((DateTimeOffset)request.EndDate.Value).ToUnixTimeMilliseconds().ToString() });
			}
			BinanceRequest binanceRequest = new BinanceRequest {
				Url = _configuration["Binance:TradesUrl"],
				Params = parameters
			};
			string response = await _requestHandler.GetTradeData(binanceRequest);
			if (response == null) {
				throw new Exception("Binance request. Empty response.");
			}
			return JsonConvert.DeserializeObject<IEnumerable<BinanceTrade>>(response);
		}
	}
}
