using Maid.Binance.DB;
using Maid.Core.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maid.Binance
{
	public class BinanceTradesLoader : IBinanceTradesLoader
	{
		private const int LIMIT = 1000;
		private readonly IBinanceTradesRequest _request;
		private readonly IEntityRepository<BinanceTrade> _tradeRepository;
		

		public BinanceTradesLoader(
				IBinanceTradesRequest request,
				IEntityRepository<BinanceTrade> tradeRepository) {
			_request = request;
			_tradeRepository = tradeRepository;
		}

		private BinanceTrade GetLastTrade() {
			return _tradeRepository.GetAll(new SelectOptions() {
				Count = 1,
				Offset = 0,
				OrderOptions = [new OrderOptions() { Column = "Time", IsAscending = false }]
			}).FirstOrDefault();
		}

		public async Task<IEnumerable<BinanceTrade>> LoadNewTrades() {
			BinanceTrade lastTrade = GetLastTrade();
			var currentYearDate = new DateTime(2025, 4, 1);
			DateTime? lastTradeDate = lastTrade != null ? DateTimeOffset.FromUnixTimeMilliseconds(lastTrade.Time + 1).DateTime : null;
			var requestParams = new LoadTradesRequest {
				StartDate = lastTradeDate != null ? new DateTime(lastTradeDate.Value.Year, lastTradeDate.Value.Month, lastTradeDate.Value.Day) : currentYearDate,
				Limit = LIMIT
			};
			var startDate = requestParams.StartDate.Value;
			requestParams.EndDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 23, 59, 59);
			var allTrades = new List<BinanceTrade>();
			while (true) {
				var trades = await _request.RequestTrades(requestParams);
				trades = trades.OrderBy(t => t.Time).ToList();
				if (trades.Any()) {
					allTrades.AddRange(trades);
				}
				if (requestParams.EndDate > DateTime.Now) {
					break;
				}
				if (trades.Count() == LIMIT) {
					trades = trades.OrderBy(t => t.Time);
					requestParams.StartDate = DateTimeOffset.FromUnixTimeMilliseconds(trades.Last().Time + 1).DateTime.ToLocalTime();
				} else { 
					requestParams.StartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 00, 00, 00).AddDays(1);
				}
				startDate = requestParams.StartDate.Value;
				requestParams.EndDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 23, 59, 59);
			}
			return allTrades.OrderBy(t => t.Time);
		}

	}
}
