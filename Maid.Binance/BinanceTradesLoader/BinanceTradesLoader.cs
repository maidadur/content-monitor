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
			var requestParams = new LoadTradesRequest {
				StartDate = lastTrade != null ? DateTimeOffset.FromUnixTimeMilliseconds(lastTrade.Time + 1000).DateTime : null,
				Limit = LIMIT
			};
			var allTrades = new List<BinanceTrade>();
			while (true) {
				var trades = await _request.RequestTrades(requestParams);
				trades = trades.OrderBy(t => t.Time).ToList();
				if (trades.Any()) {
					allTrades.AddRange(trades);
				}
				if (trades.Count() != LIMIT || !trades.Any()) {
					break;
				}
				// By default - Binance returns trades collection starting from new trades.
				// If my storage of trades is empty - fetch all trades from the recent ones to the oldest.
				// If there were some trades in storage - fetch trades starting from last one and to recent ones.
				if (lastTrade != null) {
					var lastTradeTime = DateTimeOffset.FromUnixTimeMilliseconds(trades.Last().Time).DateTime;
					requestParams.StartDate = lastTradeTime.AddMilliseconds(1);
				} else {
					var lastTradeTime = DateTimeOffset.FromUnixTimeMilliseconds(trades.First().Time).DateTime;
					requestParams.StartDate = lastTradeTime.AddDays(-7);
					requestParams.EndDate = lastTradeTime.AddMilliseconds(-1);
				}
			}
			return allTrades.OrderBy(t => t.Time);
		}

	}
}
