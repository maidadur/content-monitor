using Lib.Net.Http.WebPush;
using Maid.Core.DB;
using Maid.Notifications.DB;
using Microsoft.AspNetCore.Mvc;

namespace Maid.Notifications.Api.Controllers
{
	[Route("api/subscriptions")]
	[ApiController]
	public class PushSubscriptionsController : ControllerBase
	{
		private readonly IEntityRepository<Subscription> _pushSubscriptionsService;

		public PushSubscriptionsController(IEntityRepository<Subscription> pushSubscriptionsService) {
			_pushSubscriptionsService = pushSubscriptionsService;
		}

		[HttpPost]
		public void Post([FromBody] PushSubscription subscription) {
			_pushSubscriptionsService.Create(new Subscription(subscription));
		}

		[HttpDelete()]
		public void Delete([FromBody] PushSubscription subscription) {
			_pushSubscriptionsService.Delete(new Subscription(subscription));
		}
	}
}
