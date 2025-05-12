namespace Maid.Binance.API
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Identity.Web.Resource;
	using Maid.Core;
	using Maid.Binance;
	using Maid.Binance.DB;
	using Maid.Core.DB;
	using Maid.RabbitMQ;

	[Route("order-tags/api")]
	[ApiController]
	[Authorize]
	[RequiredScope(AcceptedScope = ["tasks.read", "tasks.write"])]
	public class BinanceTagsController : BaseApiController<BinanceOrderTag>
	{
		private IBinanceRequestHandler _requestHandler;
		private IMessageClient _messageClient { get; }


		public BinanceTagsController(
				IEntityRepository<BinanceOrderTag> repository
				) : base(repository) {

		}

		[HttpGet("list/{id:guid}")]
		public virtual async Task<ActionResult<IEnumerable<BinanceOrderTag>>> GetItemsByOrderId(Guid id) {
			var items = await EntityRepository.GetByAsync(e => e.BinanceOrderId == id);
			if (items == null) {
				return NotFound(id);
			}
			return Ok(items);
		}
	}
}