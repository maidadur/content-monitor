namespace Maid.Content.API.Controllers
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Content.DB;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Identity.Web.Resource;
	using Maid.RabbitMQ;

	[Route("api/contentsource")]
	[ApiController]
	[Authorize]
	[RequiredScope(AcceptedScope = new[] { "tasks.read", "tasks.write" })]
	public class ContentSourceController : BaseApiController<ContentSource>
	{
		protected IMessageClient MessageClient { get; }

		public ContentSourceController(IEntityRepository<ContentSource> entityRepository, IMessageClient messageClient) : base(entityRepository) {
			MessageClient = messageClient;
		}

		public override ActionResult AddItem(ContentSource item) {
			var result = base.AddItem(item);
			if (result.GetType() == typeof(OkResult)) {
				MessageClient.SendMessage("save_image", new SaveImageMessage {
					EntityId = item.Id,
					ImageUrl = item.ImageUrl,
					EntityName = item.GetType().AssemblyQualifiedName
				});
			}
			return result;
		}
	}
}