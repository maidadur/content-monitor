using Maid.Binance.DB;
using Maid.ChatGPT;
using Maid.Core;
using Maid.Core.DB;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maid.Binance
{
	public class GenerateOrderAISummaryTask : IGenerateOrderAISummaryTask
	{
		private readonly IEntityRepository<BinanceOrder> _orderRepository;
		private readonly IEntityRepository<BinanceOrderEmotion> _emotionsRepository;
		private readonly IEntityRepository<BinanceOrderTag> _tagRepository;
		private readonly ChatGptClient _gptClient;
		private readonly IConfiguration _configuration;

		public GenerateOrderAISummaryTask(
			IEntityRepository<BinanceOrder> orderRepository,
			IEntityRepository<BinanceOrderEmotion> emotionsRepository,
			IEntityRepository<BinanceOrderTag> tagRepository,
			ChatGptClient gptClient,
			IConfiguration configuration
			) {
			_orderRepository = orderRepository;
			_emotionsRepository = emotionsRepository;
			_tagRepository = tagRepository;
			_gptClient = gptClient;
			_configuration = configuration;
		}


		public async Task GenerateSummary() {
			var orders = _orderRepository.GetBy(o => o.AISummary == null && o.ImageUrl != null);
			await orders.ForEachAsync(async (order) => {
				string prompt = _configuration["GenerateOrderAISummaryPrompt"];
				var emotions = _emotionsRepository.GetBy(e => e.BinanceOrderId == order.Id);
				var tags = _tagRepository.GetBy(e => e.BinanceOrderId == order.Id);
				string emotionsString = emotions.Any() ? "Emotions: " + string.Join(",", emotions.Select(e => e.Emotion)) : "";
				string tagsString = tags.Any() ? "Tags: " + string.Join(",", tags.Select(t => t.Tag)) : "";
				prompt = prompt + "Trade: \n" +
					$"Symbol: {order.Symbol};\n" +
					$"Side: {order.Side};\n" +
					$"Pnl: {order.Pnl};\n" +
					$"Time: {order.Time};\n" +
					$"Notes: {order.Notes};\n" +
					$"Leverage: {order.Leverage};\n" +
					$"{emotionsString};\n" +
					$"{tagsString};\n";
				var response = await _gptClient.SendTextAndImageAsync(prompt, order.ImageUrl);
				order.AISummary = response;
				_orderRepository.Update(order);
			});
			if (orders.Any()) {
				_orderRepository.Save();
			}
		}

	}
}
