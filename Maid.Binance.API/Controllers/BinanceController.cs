namespace Maid.Binance.API
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Identity.Web.Resource;
	using System.Threading.Tasks;
	using System;
	using Maid.Core;
	using Maid.Binance;
	using Maid.Binance.DB;
	using Maid.Core.DB;
	using Maid.RabbitMQ;
	using Maid.Core.Utilities;

	public class GetOrdersRequest
	{
		public DateTime StartDate { get; set; }
		public DateTime DueDate { get; set; }
	}

	[Route("binance/api")]
	[ApiController]
	[Authorize]
	[RequiredScope(AcceptedScope = ["tasks.read", "tasks.write"])]
	public class BinanceController : BaseApiController<BinanceOrder>
	{
		private IBinanceRequestHandler _requestHandler;
		private IMessageClient _messageClient { get; }


		public BinanceController(
				IBinanceRequestHandler requestHandler,
				IEntityRepository<BinanceOrder> repository,
				IMessageClient messageClient
				) : base(repository) {
			_requestHandler = requestHandler;
			_messageClient = messageClient;

		}

		[HttpPost("request")]
		public async Task<ActionResult> ApiRequest([FromBody] BinanceRequest item) {
			string content = await _requestHandler.GetTradeData(item);
			return Content(content, "application/json");
		}

		[HttpPost("orders")]
		public async Task<ActionResult<IEnumerable<BinanceOrder>>> GetOrders([FromBody] GetOrdersRequest request) {
			var orders = await EntityRepository.GetByAsync(o => o.Time >= request.StartDate && o.Time <= request.DueDate,
				new SelectOptions {
					OrderOptions = [new OrderOptions {
						Column = "Time",
						IsAscending = true
					}]
				});
			var groupedOrders = orders.GroupBy(o => o.Quantity);
			var resultOrders = groupedOrders.Select(o => new BinanceOrder {
				Id = o.Where(or => or.Pnl != 0).FirstOrDefault()?.Id ?? o.First().Id,
				OrderId = o.Where(or => or.Pnl != 0).FirstOrDefault()?.OrderId ?? o.First().OrderId,
				Time = o.Where(or => or.Pnl != 0).FirstOrDefault()?.Time ?? o.First().Time,
				Pnl = o.Sum(t => t.Pnl),
				CleanPnl = o.Sum(t => t.Pnl) - o.Sum(t => t.Commission),
				Quantity = o.First().Quantity,
				Commission = o.Sum(t => t.Commission),
				CommissionAsset = o.First().CommissionAsset,
				Side = o.Where(or => or.Pnl == 0).FirstOrDefault()?.Side,
				Symbol = o.First().Symbol
			}).OrderBy(o => o.Time).Where(o => o.Pnl != 0);
			return Ok(resultOrders);
		}

		public override async Task<ActionResult<BinanceOrder>> GetItem(Guid id) {
			var item = await EntityRepository.GetAsync(id);
			if (item == null) {
				return NotFound(id);
			}
			var secondOrder = (await EntityRepository.GetByAsync(order =>
				order.Quantity == item.Quantity && order.Symbol == item.Symbol))
				.FirstOrDefault();
			if (secondOrder != null) {
				item.CleanPnl = item.Pnl - item.Commission - secondOrder.Commission;
				item.Side = secondOrder.Side;
			}
			return item;
		}

		public override ActionResult EditItem(BinanceOrder item) {
			var result = base.EditItem(item);
			if (ImageUtils.IsBase64Image(item.ImageUrl) && result.GetType() == typeof(OkResult)) {
				_messageClient.SendMessage("save_image_binance", new SaveImageMessage {
					EntityId = item.Id,
					ImageUrl = item.ImageUrl,
					EntityName = item.GetType().AssemblyQualifiedName,
					ContainerName = "binanceimage",
					CallbackQueueName = "load_image_binance"
				});
			}
			return result;
		}
	}
}