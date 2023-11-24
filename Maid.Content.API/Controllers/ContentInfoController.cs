namespace Maid.Content.API.Controllers
{
	using Maid.Core;
	using Maid.Core.DB;
	using Maid.Content;
	using Maid.Content.DB;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Identity.Web.Resource;
	using System.Threading.Tasks;
	using Maid.RabbitMQ;
	using Newtonsoft.Json;

	[Route("api/contentinfo")]
	[ApiController]
	[Authorize]
	[RequiredScope(AcceptedScope = new[] { "tasks.read", "tasks.write" })]
	public class ContentInfoController : BaseApiController<ContentInfo>
	{
		private IContentLoader _contentLoader;
		private readonly IMessageClient _messageClient;
		private IEntityRepository<ContentItemInfo> _contentItemsRep;

		public ContentInfoController(
				IEntityRepository<ContentInfo> contentInfoRep,
				IEntityRepository<ContentItemInfo> contentItemsRep,
				IContentLoader contentLoader,
				IMessageClient messageClient) : base(contentInfoRep) {
			_contentLoader = contentLoader;
			_messageClient = messageClient;
			_contentItemsRep = contentItemsRep;
		}

		[HttpPost("LoadContentInfo")]
		public async Task<ActionResult> LoadContentInfo([FromBody] ContentInfo item) {
			if (item == null) {
				return BadRequest("Content info is nulll");
			}
			item = await _contentLoader.LoadContentInfoAsync(item);
			_contentItemsRep.Delete(chapter => chapter.ContentInfo.Id == item.Id);
			_contentItemsRep.Save();
			EntityRepository.Update(item);
			EntityRepository.Save();
			var message = JsonConvert.SerializeObject(new SaveImageMessage {
				EntityId = item.Id,
				EntityName = item.GetType().AssemblyQualifiedName,
				ImageUrl = item.ImageUrl
			}).ToBytesArray();
			_messageClient.SendMessage("save_image", message);
			return Ok();
		}
	}
}