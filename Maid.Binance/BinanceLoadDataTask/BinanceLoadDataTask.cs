using Maid.Binance.DB;
using Maid.Core;
using Maid.Core.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maid.Binance
{
	public class BinanceLoadDataTask : IBinanceLoadDataTask
	{
		private IEntityRepository<BinanceTrade> _tradeRepository;
		private IEntityRepository<BinanceOrder> _orderRepository;
		private IBinanceTradesLoader _tradesLoader;
		private Dictionary<long, bool> _existingOrders = new Dictionary<long, bool>();

		public BinanceLoadDataTask(
			IEntityRepository<BinanceTrade> tradeRepository,
			IEntityRepository<BinanceOrder> orderRepository,
			IBinanceTradesLoader tradesLoader
			) {
			_tradeRepository = tradeRepository;
			_orderRepository = orderRepository;
			_tradesLoader = tradesLoader;
		}

		private void SaveOrders(List<BinanceOrder> orders) {
			orders.ForEach(o => {
				_orderRepository.Create(o);
			});
			if (orders.Any()) {
				_orderRepository.Save();
			}
		}

		private static List<BinanceOrder> MergeTradesToOrders(IEnumerable<BinanceTrade> trades) {
			var groupedTrades = trades.GroupBy(t => t.OrderId).ToList();
			var orders = new List<BinanceOrder>();
			groupedTrades.ForEach(gt => {
				var order = new BinanceOrder {
					OrderId = gt.Key,
					Pnl = gt.Sum(t => t.Pnl),
					Quantity = gt.Sum(t => t.Quantity),
					Commission = gt.Sum(t => t.Commission),
					CommissionAsset = gt.First().CommissionAsset,
					Side = gt.First().Side,
					Symbol = gt.First().Symbol,
					Time = DateTimeOffset.FromUnixTimeMilliseconds(gt.First().Time).DateTime
				};
				orders.Add(order);
			});
			return orders;
		}

		private IEnumerable<BinanceTrade> SaveTrades(IEnumerable<BinanceTrade> trades) {
			int offset = 0;
			int count = 100;
			var savedTrades = new List<BinanceTrade>();
			while (true) {
				var tradesChunk = trades.Take(new Range(offset, offset + count));
				offset += tradesChunk.Count();
				tradesChunk.ForEach(tc => {
					bool tradeExists = false;
					if (_existingOrders.ContainsKey(tc.OrderId)) {
						tradeExists = _existingOrders[tc.OrderId];
					} else {
						_existingOrders[tc.OrderId] = tradeExists = _orderRepository.GetBy(t => t.OrderId == tc.OrderId).Any();
					}
					if (!tradeExists) {
						_tradeRepository.Create(tc);
						savedTrades.Add(tc);
					}
				});
				if (tradesChunk.Count() < count) {
					break;
				} else {
					_tradeRepository.Save();
				}
			}
			return savedTrades;
		}

		public async Task LoadData() {
			var trades = await _tradesLoader.LoadNewTrades();
			var savedTrades = SaveTrades(trades);
			List<BinanceOrder> orders = MergeTradesToOrders(savedTrades);
			SaveOrders(orders);
		}

	}
}
